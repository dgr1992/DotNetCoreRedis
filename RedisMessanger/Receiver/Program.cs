using System;
using ServiceStack.Redis;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RedisManagerPool redisManager = new RedisManagerPool("localhost:63790"))
            {
                using (IRedisClient redisClient = redisManager.GetClient())
                {
                    try
                    {
                        Console.WriteLine("#### Listening to channel 'Chat' ####");
                        string msg = "";
                        while(true)
                        {
                            msg = redisClient.BlockingRemoveStartFromList("Chat", new TimeSpan(0));
                            Console.WriteLine(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
