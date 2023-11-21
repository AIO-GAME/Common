## FTP发送请求过程

### 常用 Method

| WebRequestMethods.Ftp                          | Value   | Description     |
|------------------------------------------------|:--------|:----------------|
| WebRequestMethods.Ftp.MakeDirectory            | MDK     | 创建文件夹           | 
| WebRequestMethods.Ftp.RemoveDirectory          | RMD     | 删除文件夹(要求空目录)    |
| WebRequestMethods.Ftp.DeleteFile               | DELE    | 删除文件            |
| WebRequestMethods.Ftp.Rename                   | RENAME  | 文件/文件夹 重命名      |
| WebRequestMethods.Ftp.ListDirectory            | NLST    | 获取目录列表          |
| WebRequestMethods.Ftp.ListDirectoryDetails     | LIST    | 获取目录列表(详细信息)    |
| WebRequestMethods.Ftp.DownloadFile             | RETR    | 下载文件            |
| WebRequestMethods.Ftp.UploadFile               | STOR    | 上传文件            |
| WebRequestMethods.Ftp.UploadFileWithUniqueName | STOU    | 上传唯一ID文件        |
| WebRequestMethods.Ftp.AppendFile               | APPE    | 附加文件内容          |
| WebRequestMethods.Ftp.GetFileSize              | SIZE    | 获取文件大小          |
| WebRequestMethods.Ftp.PrintWorkingDirectory    | PWD     | 输出工作目录          |

### 使用示例

公共变量

```csharp
const string server = "www.xxxx.com";
const string user = "username";
const string pass = "password";
const string rootDir = "root";
using var handle = AHandle.FTP.Create(server, user, pass, rootDir);
await handle.InitAsync(); // 初始化 FTP 连接
```

###### Download

```csharp
var progress = new ProgressArgs();
progress.OnProgress += sender => Console.WriteLine(sender);
progress.OnError += Console.WriteLine;
progress.OnComplete += Console.WriteLine;

/* 同步 */ 
handle.DownloadDir("localPath", progress); 
handle.DownloadDir("remotePath", "localPath", progress); 

/* 同步 */ 
handle.DownloadFile("localPath", progress); 
handle.DownloadFile("remotePath", "localPath", progress); 

/* 异步 */ 
await handle.DownloadDirAsync("localPath", progress);
await handle.DownloadDirAsync("remotePath", "localPath", progress);

/* 异步 */ 
await handle.DownloadFileAsync("localPath", progress);
await handle.DownloadFileAsync("remotePath", "localPath", progress);
```

###### Upload

```csharp
var progress = new ProgressArgs();
progress.OnProgress += sender => Console.WriteLine(sender);
progress.OnError += Console.WriteLine;
progress.OnComplete += Console.WriteLine;

/* 同步 */ 
handle.UploadDir("localPath", progress); 
handle.UploadDir("remotePath", "localPath", progress); 

handle.UploadFile("localPath", progress); 
handle.UploadFile("remotePath", "localPath", progress); 

/* 异步 */ 
await handle.UploadDirAsync("localPath", progress);
await handle.UploadDirAsync("remotePath", "localPath", progress); 

await handle.UploadFileAsync("localPath", progress);
await handle.UploadFileAsync("remotePath", "localPath", progress); 
```

###### Check

```csharp
/* 同步 */ 
handle.Check("remotePath"); 

handle.CheckDir("remotePath"); 

handle.CheckFile("remotePath"); 

/* 异步 */ 
await handle.CheckAsync("remotePath"); 

await handle.CheckDirAsync("remotePath"); 

await handle.CheckFileAsync("remotePath"); 
```

###### Create

```csharp
/* 同步 */ 
handle.CreateDir("remotePath"); 

/* 异步 */ 
await handle.CreateDirAsync(); 
await handle.CreateDirAsync("remotePath"); 
```

###### Delete

```csharp
/* 同步 */ 
handle.DeleteDir("remotePath"); 

handle.DeleteFile("remotePath"); 

/* 异步 */ 
await handle.DeleteDirAsync("remotePath"); 

await handle.DeleteFileAsync("remotePath"); 
```

###### Move

```csharp
/* 同步 */ 
handle.Move("currentRemoteName", "newRemoteName"); 

/* 异步 */ 
await handle.MoveAsync("currentRemoteName", "newRemoteName"); 

```

###### Get

```csharp
/* 同步 */ 
handle.GetList("remotePath"); 

handle.GetListFile("remotePath"); 

handle.GetListDir("remotePath"); 

handle.GetFileSize("remotePath"); 

/* 异步 */ 
await handle.GetListAsync("remotePath"); 

await handle.GetListFileAsync("remotePath"); 

await handle.GetListDirAsync("remotePath"); 

await handle.GetFileSizeAsync("remotePath"); 

```