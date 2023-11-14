#### SendData
~~~ C#
const string remote = "https://oapi.dingtalk.com/robot/send?access_token=xxxxx";
const string secret = "xxxx";
var data = PrDingding.CreateData();
// ToLink
data.ToLink("title", "content", "messageUrl","picUrl");
PrDingding.SendData(data, remote, secret).Sync();

// ToText
data.ToText("content");
PrDingding.SendData(data, remote, secret).Sync();

// ToMarkdown
data.ToMarkdown("title", "content");
PrDingding.SendData(data, remote, secret).Sync();

// ToActionCard
data.ToActionCard("title", "时代的火车向前开1", new Dictionary<string, string>(){
    {"阅读全文1", "https://www.dingtalk.com/"},
    {"阅读全文2", "https://www.dingtalk.com/"}
}, {0 or 1});
PrDingding.SendData(data, remote, secret).Sync();

// ToActionCard
data.ToActionCard("title", "时代的火车向前开1", "阅读全部", "https://www.dingtalk.com/");
PrDingding.SendData(data, remote, secret).Sync();

// ToFeedCard
data.ToFeedCard(new Tuple<string, string, string>[]
{
    new Tuple<string, string, string>(
        "时代的火车向前开1", 
        "https://www.dingtalk.com/", 
        "https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png"
    )
});

// ToMarkdown
data.ToMarkdown("title", "content");
PrDingding.SendData(data, remote, secret).Sync();

~~~
#### Send Markdown
~~~ C#
const string remote = "https://oapi.dingtalk.com/robot/send?access_token=xxxxx";
const string secret = "xxxx";
PrDingding.SendMarkdown("title", "content", remote, secret).Sync();
~~~
#### Send Text
~~~ C#
const string remote = "https://oapi.dingtalk.com/robot/send?access_token=xxxxx";
const string secret = "xxxx";
PrDingding.SendText("content", remote, secret).Sync();
~~~
#### Send Link
~~~ C#
const string remote = "https://oapi.dingtalk.com/robot/send?access_token=xxxxx";
const string secret = "xxxx";
PrDingding.SendLink("title", "content", "messageUrl","picUrl", remote, secret).Sync();
~~~
#### Send ActionCard
~~~ C#
const string remote = "https://oapi.dingtalk.com/robot/send?access_token=xxxxx";
const string secret = "xxxx";
PrDingding.SendActionCard("title", "时代的火车向前开1", new Dictionary<string, string>(){
    {"阅读全文1", "https://www.dingtalk.com/"},
    {"阅读全文2", "https://www.dingtalk.com/"}
}, {0 or 1}, remote, secret).Sync();
// or 
PrDingding.SendActionCard("title", "content", "singleTitle", "singleURL", remote, secret).Sync();
~~~
#### Send FeedCard
~~~ C#
const string remote = "https://oapi.dingtalk.com/robot/send?access_token=xxxxx";
const string secret = "xxxx";
PrDingding.SendFeedCard(new Tuple<string, string, string>[]
{
    new Tuple<string, string, string>(
        "时代的火车向前开1", 
        "https://www.dingtalk.com/", 
        "https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png"
    )
}, remote, secret).Sync();
~~~