/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-29
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 进度参数
    /// </summary>
    public partial class AProgress : IDisposable
    {
        /// <summary>
        /// 精度器
        /// </summary>
        private Stopwatch Watch;

        /// <summary>
        /// 间隔数量
        /// </summary>
        private long _IntervalQuantity;

        /// <summary>
        /// 当前值
        /// </summary>
        private long _CurrentValue;

        /// <summary>
        /// 缓存当前值
        /// </summary>
        private long _CatchCurrent;

        /// <summary>
        /// 开始值
        /// </summary>
        private long _StartValue;

        /// <summary>
        /// 记录时间
        /// </summary>
        private DateTime RecordTime { get; set; }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            var tempSecond = Watch.Elapsed.TotalSeconds;
            if (tempSecond < 1) return;
            Watch.Stop();
            _IntervalQuantity = _CurrentValue - _CatchCurrent;
            if (_IntervalQuantity > 0) Speed = (long)(_IntervalQuantity / tempSecond);
            else Speed = 0;
            _CatchCurrent = _CurrentValue;
            Watch.Restart();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AProgress()
        {
            State = EProgressState.Ready;
            _StartValue = _CurrentValue = _CatchCurrent = TotalValue = Speed = 0;
            Watch = new Stopwatch();
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Begin()
        {
            RecordTime = StartTime = DateTime.UtcNow;
            _CatchCurrent = _CurrentValue = _StartValue;
            StartProgress = Progress;
            Watch.Start();
        }


        /// <summary>
        /// 完成
        /// </summary>
        public void Complete()
        {
            Watch.Stop();
            RemainingTime += DateTime.UtcNow - RecordTime;
            EndTime = DateTime.UtcNow;
            LastProgress = Progress;
            EndValue = _CurrentValue;
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Resume()
        {
            RecordTime = DateTime.UtcNow;
            _CatchCurrent = _CurrentValue;
            Watch.Start();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            Watch.Stop();
            RemainingTime += DateTime.UtcNow - RecordTime;
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            Watch.Stop();
            RemainingTime += DateTime.UtcNow - RecordTime;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            OnComplete?.Invoke(this);
            Watch = null;
            OnProgress = null;
            OnBegin = null;
            OnComplete = null;
            OnError = null;
            OnResume = null;
            OnPause = null;
            OnCancel = null;
        }

        /// <inheritdoc />
        public sealed override string ToString()
        {
            return State == EProgressState.Running
                ? ((IProgressInfo)this).ToString()
                : ((IProgressReport)this).ToString();
        }

        /// <inheritdoc />
        public sealed override bool Equals(object obj)
        {
            return false;
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            return 0;
        }
    }

    public partial class AProgress : IProgressEvent
    {
        /// <inheritdoc />
        public Action<IProgressInfo> OnProgress { get; set; }

        /// <inheritdoc />
        public Action OnBegin { get; set; }

        /// <inheritdoc />
        public Action<IProgressReport> OnComplete { get; set; }

        /// <inheritdoc />
        public Action<Exception> OnError { get; set; }

        /// <inheritdoc />
        public Action OnResume { get; set; }

        /// <inheritdoc />
        public Action OnPause { get; set; }

        /// <inheritdoc />
        public Action OnCancel { get; set; }
    }

    public partial class AProgress : IProgressInfo
    {
        /// <inheritdoc />
        public long TotalValue
        {
            get => _Total + _StartValue;
            set => _Total = value;
        }

        private long _Total;

        /// <inheritdoc />
        public long Speed { get; private set; }

        /// <inheritdoc />
        public string CurrentInfo { get; internal set; }

        /// <inheritdoc />
        public string CurrentStr => CurrentValue.ToConverseStringFileSize();

        /// <inheritdoc />
        public string TotalStr => TotalValue.ToConverseStringFileSize();

        /// <inheritdoc />
        public long CurrentValue
        {
            get => _CurrentValue;
            set
            {
                _CurrentValue = value;
                Update();
                OnProgress?.Invoke(this);
            }
        }

        /// <inheritdoc />
        public int Progress
        {
            get
            {
                if (TotalValue <= 0) return 0;
                return (int)(_CurrentValue / (double)TotalValue * 100);
            }
        }

        /// <inheritdoc />
        string IProgressInfo.ToString()
        {
            return $"{Progress}% [{CurrentStr}/{TotalStr}] {CurrentInfo} {Speed.ToConverseStringFileSize()}/s";
        }
    }

    public partial class AProgress : IProgressReport
    {
        /// <inheritdoc />
        public EProgressState State { get; set; }

        /// <inheritdoc />
        public long AverageSpeed => (long)(VirtualValue / RemainingTime.TotalSeconds);

        /// <inheritdoc />
        public TimeSpan RemainingTime { get; set; }

        /// <inheritdoc />
        public DateTime StartTime { get; set; }

        /// <inheritdoc />
        public DateTime EndTime { get; set; }

        /// <inheritdoc />
        public long LastProgress { get; set; }

        /// <inheritdoc />
        public long StartProgress { get; set; }

        /// <inheritdoc />
        public long VirtualValue => EndValue - StartValue;

        /// <inheritdoc />
        public long VirtualProgress => LastProgress - StartProgress;

        /// <inheritdoc />
        public long EndValue { get; set; }

        /// <inheritdoc />
        public long StartValue
        {
            get => _StartValue;
            set
            {
                if (value <= 0) return;
                EndValue = _CatchCurrent = _CurrentValue = _StartValue = value;
                StartProgress = Progress;
            }
        }

        /// <inheritdoc cref="IProgressReport.ToString" />
        string IProgressReport.ToString()
        {
            var str = new StringBuilder();
            str.AppendLine($"{nameof(State),-16} : {State}");
            str.AppendLine($"{nameof(AverageSpeed),-16} : {AverageSpeed.ToConverseStringFileSize()}/s");
            str.AppendLine(
                $"{"From To Time",-16} : [{StartTime:yyyy-MM-dd HH:mm:ss} - {EndTime:yyyy-MM-dd HH:mm:ss}] {RemainingTime:d\\.hh\\:mm\\:ss}");
            str.AppendLine($"{"From To Progress",-16} : [{StartProgress}% - {LastProgress}%] {VirtualProgress}%");
            str.AppendLine(
                $"{"From To Value",-16} : [{StartValue.ToConverseStringFileSize()} - {EndValue.ToConverseStringFileSize()}] {VirtualValue.ToConverseStringFileSize()}");
            return str.ToString();
        }
    }
}