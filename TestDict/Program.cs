using System;
using System.Collections.Generic;

/* 
 *  1、创建一个字典dictA，其中Key为字符串，Value为整数。
    2、为这个字典添加"a"到"z"共26个Key，值为随机1~10的数字。

    1、对字典dictA，删除所有值为偶数的键值对。
    2、再次创建同样的字典dictB，也同样删除值为偶数的键。
    3、然后将这两个字典合并成一个新字典。Key相同的部分，以dictA的值为准

    1、随机生成一个string列表
    2、将这个列表的第一项作为key，第二项作为value；第三项作为key，第四项作为value……依次类推。生成一个新的字典
    3、再将这个新的字典转换成一个新的列表
 */

/*
    * 这是一个 C# 中的语句，它表示将一个键值对添加到一个字典 (dictionary) 中。
    * 其中 "dict" 是字典变量的名称，"pair" 是一个包含键值对的变量，".Key" 和 ".Value" 分别表示键和值。
        例如，如果有一个字典变量名为 "myDict"，键值对变量名为 "newPair"，键为 "key1"，值为 "value1"，
        那么这句话等价于
        myDict["key1"] = "value1";
*/
namespace TestDict
{
    class Program
    {
        static Random random = new Random();
        static string all = "abcdefghijklmnopkrstuvwxyz";

        // 生成随机字典
        static Dictionary<string, int> CreateDict()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < 15; i++)
            {
                char c = all[random.Next(0, all.Length)];
                dict[c.ToString()] = random.Next(1, 11);     // 添加元素或覆盖
            }
            return dict;
        }

        // 融合字典
        static Dictionary<string, int> MergeDict(Dictionary<string, int> dictA, Dictionary<string, int> dictB)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>(dictA);
            //foreach(var pair in dictA)
            //{
            //    dict.Add(pair.Key, pair.Value);
            //}

            foreach (var pair in dictB) 
            {
                //dict.Add(pair.Key, pair.Value);
                // 如果出现相同的Key，都会被value值覆盖，下面的一行代码决定了是以dictA还是dictB的值为基准
                dict[pair.Key] = pair.Value;
            }
            return dict;
        }

        // { a:1, b:2, c:3 }    {a:3, b:4, d:6}
        // { a:[1,3], b:[2,4], c:[3], d:[6] }

        // 如果DictA中一个Key和DictB的Key重名了，那么怎么样保留所有的Key的键呢？
        static Dictionary<string,List<int>> MergeDict2(Dictionary<string,int> dictA,Dictionary<string,int> dictB)
        {
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            foreach(var pair in dictA)
            {
                dict.Add(pair.Key, new List<int> { pair.Value });
            }

            foreach (var pair in dictB)
            {
                if(!dict.ContainsKey(pair.Key))
                {
                    dict.Add(pair.Key, new List<int> { pair.Value });
                }
                else
                {
                    dict[pair.Key].Add(pair.Value);
                }
            }

            return dict;
        }
        static void PrintDict(Dictionary<string,int> dict)
        {
            foreach(var pair in dict)
            {
                Console.Write($"{pair.Key}:{pair.Value},");
            }
            Console.WriteLine();
        }
        static void PrintDict(Dictionary<string, List<int>> dict)
        {
            foreach (var pair in dict)
            {
                Console.Write($"{pair.Key}:");
                foreach(var n in pair.Value)
                {
                    Console.Write(n + ", ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }



        static void Main(string[] args)
        {
            Dictionary<string, int> dictA = CreateDict();
            Dictionary<string, int> dictB = CreateDict();

            PrintDict(dictA);
            PrintDict(dictB);

            Dictionary<string,int> dictC = MergeDict(dictA, dictB);
            PrintDict(dictC);

            Dictionary<string, List<int>> dictD = MergeDict2(dictA, dictB);
            PrintDict(dictD);

            Console.ReadKey();
        }
    }
}
