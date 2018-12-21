using System;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.Redis;
using ServiceStack.DataAnnotations;

namespace RedisSimple
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
                        long result = -1;
                        using (IRedisTransaction redisTransaction = redisClient.CreateTransaction())
                        {
                            redisTransaction.QueueCommand(r => r.Set<int>("MyInt", 4710));
                            redisTransaction.QueueCommand(r => r.Increment("MyInt", 1), i => result = i);
                            redisTransaction.Commit();
                        }

                        Console.WriteLine("Result of transaction: " + result);

                        int value = redisClient.Get<int>("MyInt");
                        Console.WriteLine("Get value outside of transaction: " + value);
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
