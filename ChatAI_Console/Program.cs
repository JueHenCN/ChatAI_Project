using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using TouchSocket.Sockets;

namespace ChatAI_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitWebScorSocket();
            //script.main();

            while (true)
            {
                Console.Write("\n用户:");
                string userInput = Console.ReadLine();
                if (userInput == "exit") // 例如，用 "exit" 作为退出条件
                    break;
                //scriptChat.process_query(userInput);

                client.SendWithWSAsync(userInput);

                //dynamic result = script.process_query(userInput);
                //Console.WriteLine(result[0]);
            }

        }


        private static void InitTcpSocket()
        {

        }

        private static void PythonRun()
        {
            InitPythonEngine();

            string scriptPath = "D:\\AI\\ChatGLM3-main\\basic_demo\\";
            //string chatScriptName = "new_client";
            //string webSocketScriptName = "WebSocketTest";


            PythonEngine.Initialize(); // 初始化 Python 引擎

            try
            {
                using (Py.GIL()) // 获取 Python 全局解释锁
                {
                    dynamic sys = Py.Import("sys");
                    sys.path.append(scriptPath);  // 将脚本所在目录添加到 sys.path
                    Console.WriteLine("Python 版本: " + sys.version);

                    // 设置 Python 脚本路径
                    //dynamic os = Py.Import("os");
                    //os.chdir(scriptPath);

                    // 导入 Python 脚本作为模块
                    //scriptChat.init_model();
                    //dynamic scriptWebSocket = Py.Import(webSocketScriptName);
                    //dynamic scriptChat = Py.Import(chatScriptName);

                    InitWebScorSocket();
                    //script.main();

                    while (true)
                    {
                        Console.Write("\n用户:");
                        string userInput = Console.ReadLine();
                        if (userInput == "exit") // 例如，用 "exit" 作为退出条件
                            break;
                        //scriptChat.process_query(userInput);

                        client.SendWithWSAsync(userInput);

                        //dynamic result = script.process_query(userInput);
                        //Console.WriteLine(result[0]);
                    }

                }
            }
            catch (PythonException ex)
            {
                Console.WriteLine("Python error: " + ex.Message);
            }
            finally
            {
                PythonEngine.Shutdown();
            }
        }


        static WebSocketClient client;
        private static void InitWebScorSocket()
        {
            try
            {

                client = new WebSocketClient();
                client.Setup(new TouchSocketConfig()
                    .SetRemoteIPHost("ws://127.0.0.1:9001/echo")
                    .ConfigureContainer(a =>
                    {
                        a.AddConsoleLogger();
                    }));
                client.Received = (c, e) =>
                {
                    switch (e.Opcode)
                    {
                        case WSDataType.Cont:
                            break;
                        case WSDataType.Text:
                            Console.Write(e.ToText());
                            break;
                        case WSDataType.Binary:
                            break;
                        case WSDataType.Close:
                            break;
                        case WSDataType.Ping:
                            break;
                        case WSDataType.Pong:
                            break;
                        default:
                            break;
                    }
                };

                client.ConnectAsync(10000);

                Console.WriteLine("服务端连接成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务端连接异常");
                Console.WriteLine(ex.Message);
            }

        }

        private static void InitPythonEngine()
        {
            string dllPath = "C:\\Users\\CN_JueHen\\AppData\\Local\\Programs\\Python\\Python311\\python311.dll";
            string pythonPath = "C:\\Users\\CN_JueHen\\AppData\\Local\\Programs\\Python\\Python311";

            //string dllPath = "D:\\AI\\py311\\python311.dll";
            //string pythonPath = "D:\\AI\\py311";

            Runtime.PythonDLL = dllPath; // 指定你的 Python DLL 文件的路径
            PythonEngine.PythonHome = pythonPath; // 指定你的 Python 安装路径
        }
    }
}
