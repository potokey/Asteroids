using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Aggregator
{
    static List<BaseModel> SubscriptionList = new List<BaseModel>();
    public static void Subscribe<T>(string subscriptionName, Action<T> action)
    {
        SubscriptionList.Add(
            new Model<T>()
            {
                subscriptionName = subscriptionName,
                Type = typeof(T),
                Action = action,
            });
    }

    public static void Publish<T>(string subscriptionName, T request)
    {
        var subscriptions = SubscriptionList.Where(f => f.subscriptionName == subscriptionName && f.Type == typeof(T));
        foreach (var subscription in subscriptions)
        {
            var model = (Model<T>)subscription;
            model.Action.Invoke(request);
        }
    }

    private class BaseModel
    {
        public string subscriptionName { get; set; }
        public Type Type { get; set; }
    }

    private class Model<T> : BaseModel
    {
        public Action<T> Action;
    }
}
