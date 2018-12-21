using System;
using ServiceStack.Redis;

namespace Sender
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
                        Console.WriteLine("#### Sending to channel 'Chat' ####");
                        string msg = "";
                        while(true)
                        {   
                            Console.Write("Message to 'Chat': ");
                            msg = Console.ReadLine();
                            redisClient.AddItemToList("Chat", msg);
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
