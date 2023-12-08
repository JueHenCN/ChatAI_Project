using ChatAI_Connect.LogManage;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ChatAI_Connect.GlobalManage
{
    public class GlobalConfig
    {
        private static readonly string _configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config", "config.json");

        public static LocalConfig Config;

        public static bool LoadConfig()
        {
            try
            {
                // 提取文件夹路径和文件名
                string folderPath = Path.GetDirectoryName(_configPath);

                // 检查文件夹是否存在
                if (!Directory.Exists(folderPath))
                {
                    // 文件夹不存在，创建文件夹
                    Directory.CreateDirectory(folderPath);
                    LogHandler.Debug($"文件夹 {folderPath} 已创建。");
                }


                // 检查文件是否存在
                if (!File.Exists(_configPath))
                {
                    // 文件不存在，创建文件
                    using (File.Create(_configPath)) { }

                    Config = new LocalConfig
                    {
                        IpAddress = "127.0.0.1",
                        Port = 8765,
                        PythonPath = Path.Combine(Directory.GetCurrentDirectory(), "py3116"),
                        SessionFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Sessions"),
                        ModelPath = Path.Combine(Directory.GetCurrentDirectory(), "models")
                    };
                    if (!SaveConfig())
                    {
                        return false;
                    }
                }
                else
                {
                    Config = JsonConvert.DeserializeObject<LocalConfig>(File.ReadAllText(_configPath));
                }

                LogHandler.Info("载入本地文件成功");
                return true;
            }
            catch (Exception ex)
            {
                LogHandler.Error($"载入本地文件异常:{ex.Message}");
                return false;
            }

        }

        public static bool SaveConfig()
        {
            try
            {
                File.WriteAllText(_configPath, JsonConvert.SerializeObject(Config));
                LogHandler.Info("保存配置成功");
                return true;
            }
            catch (Exception ex)
            {
                LogHandler.Error($"保存文件异常:{ex.Message}");
                return false;
            }
        }

    }

    public class LocalConfig
    {
        public string IpAddress;

        public int Port;

        public string PythonPath;

        public string ModelPath;

        public string SessionFolderPath;
    }
}
