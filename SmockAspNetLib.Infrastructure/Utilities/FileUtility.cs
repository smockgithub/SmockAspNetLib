//using System;
//using System.Collections;
//using System.Collections.Specialized;
//using System.Data;
//using System.Diagnostics;
//using System.IO;
//using System.Text;
//using System.Text.RegularExpressions;

//using SmockAspNetLib.Infrastructure.Types;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    /// <summary>
//    /// 文件通用类
//    /// </summary>
//    public class FileUtility
//    {

//        private static string fCurrentPath = string.Empty;

//        /// <summary>
//        /// 向文件添加内容
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="text"></param>
//        public static void AppendFile(string fileName, string text)
//        {
//            if (File.Exists(fileName))
//            {
//                StreamWriter writer = null;
//                try
//                {
//                    writer = new StreamWriter(fileName, true);
//                    writer.WriteLine(text);
//                }
//                finally
//                {
//                    writer.Flush();
//                    writer.Close();
//                }
//            }
//            else
//            {
//                CreateFile(fileName, text);
//            }
//        }

//        /// <summary>
//        /// 判断流内信息是否相同
//        /// </summary>
//        /// <param name="s1"></param>
//        /// <param name="s2"></param>
//        /// <returns></returns>
//        public static bool CheckEqual(Stream s1, Stream s2)
//        {
//            if (((s1 == null) || (s2 == null)) || (s1.Length != s2.Length))
//            {
//                return false;
//            }
//            byte[] buffer = new byte[s1.Length];
//            s1.Read(buffer, 0, (int)s1.Length);
//            byte[] buffer2 = new byte[s2.Length];
//            s1.Read(buffer2, 0, (int)s2.Length);
//            for (int i = 0; i < buffer.Length; i++)
//            {
//                if (buffer[i] != buffer2[i])
//                {
//                    return false;
//                }
//            }
//            return true;
//        }

//        /// <summary>
//        /// 合并两个路径字符串
//        /// </summary>
//        /// <param name="partPath1"></param>
//        /// <param name="partPath2"></param>
//        /// <returns></returns>
//        public static string Combine(string partPath1, string partPath2)
//        {
//            return Path.Combine(partPath1, partPath2);
//        }

//        /// <summary>
//        /// 比较两个路径,通过引用返回结果 
//        /// </summary>
//        /// <param name="path1"></param>
//        /// <param name="path2"></param>
//        /// <param name="inPath1AndNotInPath2"></param>
//        /// <param name="inPath2AndNotInPath1"></param>
//        public void ComparePath(string path1, string path2, ref StringCollection inPath1AndNotInPath2, ref StringCollection inPath2AndNotInPath1)
//        {
//            if (ExistsPath(path1) && ExistsPath(path2))
//            {
//                path1 = FormatPath(path1);
//                path2 = FormatPath(path2);
//                foreach (string str in GetFiles(path1, false))
//                {
//                    inPath1AndNotInPath2.Add(str.ToUpper());
//                }
//                foreach (string str in GetFiles(path2, false))
//                {
//                    inPath2AndNotInPath1.Add(str.ToUpper());
//                }
//                foreach (string str in inPath1AndNotInPath2)
//                {
//                    if (inPath2AndNotInPath1.Contains(str))
//                    {
//                        inPath2AndNotInPath1.Remove(str);
//                    }
//                }
//                foreach (string str in inPath2AndNotInPath1)
//                {
//                    if (inPath1AndNotInPath2.Contains(str))
//                    {
//                        inPath1AndNotInPath2.Remove(str);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 复制文件到指定的文件夹,文件名相同，返回拷贝后的文件名（包括全路径） 
//        /// </summary>
//        /// <param name="srcFile"></param>
//        /// <param name="dstPath"></param>
//        /// <returns></returns>
//        public static string CopyFile(string srcFile, string dstPath)
//        {
//            if (!File.Exists(srcFile))
//            {
//                throw new FileNotFoundException(srcFile);
//            }
//            srcFile = FormatFile(srcFile);
//            dstPath = FormatPath(dstPath);
//            string fileName = GetFileName(srcFile);
//            CreatePath(dstPath);
//            fileName = Path.Combine(dstPath, fileName);
//            File.Copy(srcFile, fileName, true);
//            return fileName;
//        }

//        /// <summary>
//        /// 复制文件到指定的文件夹,指定文件名，返回拷贝后的文件名
//        /// </summary>
//        /// <param name="srcFile"></param>
//        /// <param name="dstPath"></param>
//        /// <param name="dstFileName"></param>
//        /// <param name="overWrite"></param>
//        /// <returns></returns>
//        public static string CopyFile(string srcFile, string dstPath, string dstFileName, bool overWrite)
//        {
//            string destFileName = Path.Combine(dstPath, dstFileName);
//            CreatePath(dstPath);
//            File.Copy(srcFile, destFileName, overWrite);
//            return destFileName;
//        }

//        /// <summary>
//        /// 把源文件夹下的文件全部复制到目标文件夹 
//        /// </summary>
//        /// <param name="fromPath"></param>
//        /// <param name="toPath"></param>
//        public static void CopyFiles(string fromPath, string toPath)
//        {
//            if (DataUtility.IsInvalid(fromPath))
//            {
//                throw new Exception("fromPath不能为空");
//            }
//            if (DataUtility.IsInvalid(toPath))
//            {
//                throw new Exception("toPath不能为空");
//            }
//            fromPath = FormatFile(fromPath.Trim());
//            toPath = FormatFile(toPath.Trim());
//            CreatePath(toPath);
//            DirectoryInfo info = new DirectoryInfo(fromPath);
//            DirectoryInfo[] directories = info.GetDirectories("*.*", SearchOption.AllDirectories);
//            foreach (DirectoryInfo info2 in directories)
//            {
//                CreatePath(toPath + info2.FullName.Remove(0, fromPath.Length));
//            }
//            FileInfo[] files = info.GetFiles("*.*", SearchOption.AllDirectories);
//            foreach (FileInfo info3 in files)
//            {
//                string fileName = toPath + info3.FullName.Remove(0, fromPath.Length);
//                FileInfo info4 = new FileInfo(fileName);
//                if (!info4.Directory.Exists)
//                {
//                    info4.Directory.Create();
//                }
//                info3.CopyTo(fileName, true);
//            }
//        }

//        /// <summary>
//        /// 复制文件到指定的文件夹,文件名相同
//        /// </summary>
//        /// <param name="srcFile"></param>
//        /// <param name="dstFile"></param>
//        /// <param name="overWrite"></param>
//        public static void CopyToFile(string srcFile, string dstFile, bool overWrite)
//        {
//            CopyFile(srcFile, GetFilePath(dstFile), GetFileName(dstFile), overWrite);
//        }

//        /// <summary>
//        /// 通过加后缀的方式复制文件，如原文件为：C:\X.doc，加上后缀1变为：C:\X1.doc。 返回新文件的全路径 
//        /// </summary>
//        /// <param name="srcFile"></param>
//        /// <param name="newFilePostfix"></param>
//        /// <param name="overWrite"></param>
//        /// <returns></returns>
//        public static string CopyToFileWithPostfix(string srcFile, string newFilePostfix, bool overWrite)
//        {
//            string dstFile = string.Format("{0}_{1}{2}", srcFile.Substring(0, srcFile.LastIndexOf(".")), newFilePostfix, srcFile.Remove(0, srcFile.LastIndexOf(".")));
//            if (overWrite)
//            {
//                CopyToFile(srcFile, dstFile, overWrite);
//            }
//            return dstFile;
//        }

//        /// <summary>
//        /// 创建文件,并且覆盖写入新的内容,如果没有文件，则创建相应的文件，以UTF8方式创建 
//        /// </summary>
//        /// <param name="fileName"></param>
//        public static void CreateFile(string fileName)
//        {
//            CreateFile(fileName, string.Empty);
//        }

//        /// <summary>
//        /// 创建文件,并且覆盖写入新的内容，如果文件没有权限，则对文件进行user用户的完全控制权限 ,如果没有文件，则创建相应的文件，以UTF8方式创建 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="text"></param>
//        public static void CreateFile(string fileName, string text)
//        {
//            CreateFile(fileName, text, Encoding.UTF8, false);
//        }

//        /// <summary>
//        /// 创建文件,并且写入新的内容,如果没有文件，则创建相应的文件，以UTF8方式创建 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="text"></param>
//        /// <param name="append"></param>
//        public static void CreateFile(string fileName, string text, bool append)
//        {
//            CreateFile(fileName, text, Encoding.UTF8, append);
//        }

//        /// <summary>
//        /// 创建指定格式的文件，并改写原来的内容 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="text"></param>
//        /// <param name="encoding"></param>
//        public static void CreateFile(string fileName, string text, Encoding encoding)
//        {
//            CreateFile(fileName, text, encoding, false);
//        }

//        /// <summary>
//        /// 创建指定格式的文件
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="text"></param>
//        /// <param name="encoding"></param>
//        /// <param name="append"></param>
//        public static void CreateFile(string fileName, string text, Encoding encoding, bool append)
//        {
//            StreamWriter writer = null;
//            try
//            {
//                string filePath = GetFilePath(fileName);
//                if (!(!(filePath.Trim() != string.Empty) || Directory.Exists(filePath)))
//                {
//                    Directory.CreateDirectory(filePath);
//                }
//                writer = new StreamWriter(fileName, append, encoding);
//                writer.WriteLine(text);
//            }
//            finally
//            {
//                if (writer != null)
//                {
//                    writer.Flush();
//                    writer.Close();
//                }
//            }
//        }

//        /// <summary>
//        /// 创建指定的目录,如果有则不需要创建. 
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public static string CreatePath(string path)
//        {
//            if (!Directory.Exists(path))
//            {
//                Directory.CreateDirectory(path);
//            }
//            return path;
//        }

//        /// <summary>
//        /// 删除指定的文件 
//        /// </summary>
//        /// <param name="fileName"></param>
//        public static void DeleteFile(string fileName)
//        {
//            if (ExistsFile(fileName))
//            {
//                File.Delete(fileName);
//            }
//        }

//        /// <summary>
//        /// 删除指定目录中的所有文件，包括所有子目录 
//        /// </summary>
//        /// <param name="filesPath"></param>
//        public static void DeleteFiles(string filesPath)
//        {
//            if (ExistsPath(filesPath))
//            {
//                string[] files = Directory.GetFiles(filesPath);
//                foreach (string str in files)
//                {
//                    SetReadOnly(str, false);
//                    File.Delete(str);
//                }
//            }
//        }

//        /// <summary>
//        /// 删除指定的路径,如果不存在，则返回
//        /// </summary>
//        /// <param name="path"></param>
//        public static void DeletePath(string path)
//        {
//            try
//            {
//                if (ExistsPath(path))
//                {
//                    DeleteFiles(path);
//                    Directory.Delete(path, true);
//                }
//            }
//            catch
//            {
//            }
//        }

//        /// <summary>
//        /// 判断是否存在指定的文件 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <returns></returns>
//        public static bool ExistsFile(string fileName)
//        {
//            return File.Exists(fileName);
//        }

//        /// <summary>
//        /// 判断是否存在指定的目录
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public static bool ExistsPath(string path)
//        {
//            return Directory.Exists(path);
//        }

//        /// <summary>
//        /// 格式化异常信息
//        /// </summary>
//        /// <param name="ex"></param>
//        /// <returns></returns>
//        public static string FormatException(Exception ex)
//        {
//            return string.Format("程序/对象名：{0}；方法：{1}；异常消息：{2}", ex.Source, ex.TargetSite.Name, ex.Message);
//        }

//        /// <summary>
//        /// 对文件的路径进行格式化，确保其中只有\ 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string FormatFile(string fullFileName)
//        {
//            return (FormatPath(GetFilePath(fullFileName)) + GetFileName(fullFileName));
//        }

//        /// <summary>
//        /// 获取格式化后的文件名，把异常字符替换为下划线 
//        /// </summary>
//        /// <param name="rawFileName"></param>
//        /// <returns></returns>
//        public static string FormatFileName(string rawFileName)
//        {
//            Regex regex = new Regex(@"[/\:* ]+");
//            return regex.Replace(rawFileName, "_");
//        }

//        /// <summary>
//        /// 格式化文件路径地址，以\结尾 
//        /// </summary>
//        /// <param name="filePath"></param>
//        /// <returns></returns>
//        public static string FormatPath(string filePath)
//        {
//            if (DataUtility.IsInvalid(filePath))
//            {
//                return string.Empty;
//            }
//            filePath = filePath.Trim();
//            filePath = filePath.Replace("/", @"\");
//            if (!filePath.EndsWith(@"\"))
//            {
//                return (filePath + @"\");
//            }
//            return filePath;
//        }

//        /// <summary>
//        /// 获取文件对应的二进制数组，以方便以二进制格式发送文件（可以用于WebService），或者保存到数据库 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <returns></returns>
//        public static byte[] GetBitFile(string fileName)
//        {
//            FileStream stream = null;
//            byte[] buffer2;
//            try
//            {
//                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
//                int length = (int)stream.Length;
//                byte[] buffer = new byte[length];
//                stream.Read(buffer, 0, length);
//                buffer2 = buffer;
//            }
//            finally
//            {
//                if (stream != null)
//                {
//                    stream.Close();
//                }
//            }
//            return buffer2;
//        }

//        /// <summary>
//        /// 获取文件的扩展名,包含分隔符.(eg .xls) 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string GetExtension(string fullFileName)
//        {
//            return Path.GetExtension(fullFileName);
//        }

//        /// <summary>
//        /// 获取指定文件的内容行数 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <returns></returns>
//        public static int GetFileLineNumber(string fileName)
//        {
//            if (!ExistsFile(fileName))
//            {
//                return 0;
//            }
//            int num = 0;
//            using (StreamReader reader = new StreamReader(fileName))
//            {
//                while (!reader.EndOfStream)
//                {
//                    reader.ReadLine();
//                    num++;
//                }
//            }
//            return num;
//        }

//        /// <summary>
//        /// 根据全路径文件名，只返回文件名，包括扩展名， 但不包括路径。 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string GetFileName(string fullFileName)
//        {
//            FileInfo info = new FileInfo(fullFileName);
//            return info.Name;
//        }

//        /// <summary>
//        /// 根据提供的包括路径的文件名，返回其对应的文件目录，以\结尾 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string GetFilePath(string fullFileName)
//        {
//            return FormatPath(Path.GetDirectoryName(fullFileName));
//        }

//        /// <summary>
//        /// 获取指定路径下的所有文件列表 
//        /// </summary>
//        /// <param name="filePath"></param>
//        /// <returns></returns>
//        public static string[] GetFiles(string filePath)
//        {
//            CreatePath(filePath);
//            return Directory.GetFiles(filePath);
//        }

//        /// <summary>
//        /// 获取指定路径下的所有文件列表 
//        /// </summary>
//        /// <param name="filePath"></param>
//        /// <param name="includeSubDirectories"></param>
//        /// <returns></returns>
//        public static string[] GetFiles(string filePath, bool includeSubDirectories)
//        {
//            return GetFiles(filePath, "*.*", includeSubDirectories);
//        }

//        /// <summary>
//        /// 获取指定路径下的所有文件列表
//        /// </summary>
//        /// <param name="filePath"></param>
//        /// <param name="filter"></param>
//        /// <param name="includeSubDirectories"></param>
//        /// <returns></returns>
//        public static string[] GetFiles(string filePath, string filter, bool includeSubDirectories)
//        {
//            CreatePath(filePath);
//            if (includeSubDirectories)
//            {
//                return Directory.GetFiles(filePath, filter, SearchOption.AllDirectories);
//            }
//            return Directory.GetFiles(filePath, filter);
//        }

//        /// <summary>
//        /// 获取指定文件的大小(Byte为单位） 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <returns></returns>
//        public static int GetFileSize(string fileName)
//        {
//            FileStream stream = null;
//            int num2;
//            try
//            {
//                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
//                int length = (int)stream.Length;
//                num2 = length;
//            }
//            finally
//            {
//                if (stream != null)
//                {
//                    stream.Close();
//                }
//            }
//            return num2;
//        }

//        /// <summary>
//        /// 根据计算单位返回文件大小  
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="sizeType"></param>
//        /// <returns></returns>
//        public static string GetFileSize(string fileName, ByteSizeType sizeType)
//        {
//            double num2 = (GetFileSize(fileName) * 1.0) / ((double)sizeType);
//            switch (sizeType)
//            {
//                case ByteSizeType.KB:
//                    if (num2 == 0.0)
//                    {
//                        num2 = 1.0;
//                    }
//                    return string.Format("{0:F0}{1}", num2, sizeType);

//                case ByteSizeType.MB:
//                case ByteSizeType.GB:
//                    return string.Format("{0:F2}{1}", num2, sizeType);
//            }
//            return string.Empty;
//        }

//        /// <summary>
//        /// 获取指定目录中所有的文件的版本信息对于 Excel，不要考虑版本了 
//        /// </summary>
//        /// <param name="filePath"></param>
//        /// <returns></returns>
//        public static Hashtable GetFilesVersion(string filePath)
//        {
//            CreatePath(filePath);
//            string[] files = Directory.GetFiles(filePath);
//            Hashtable hashtable = new Hashtable();
//            foreach (string str in files)
//            {
//                string fileName = GetFileName(str);
//                string fileVersion = FileVersionInfo.GetVersionInfo(str).FileVersion;
//                hashtable.Add(fileName, fileVersion);
//            }
//            return hashtable;
//        }

//        /// <summary>
//        /// 获取最后修改时间
//        /// </summary>
//        /// <param name="file"></param>
//        /// <returns></returns>
//        public static DateTime GetLastWriteTime(string file)
//        {
//            FileInfo info = new FileInfo(file);
//            return info.LastWriteTime;
//        }

//        /// <summary>
//        /// 根据全路径文件名，只返回文件名，不包括路径和扩展名 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string GetOnlyFileName(string fullFileName)
//        {
//            string fileName = GetFileName(fullFileName);
//            return fileName.Substring(0, fileName.LastIndexOf('.'));
//        }

//        /// <summary>
//        /// 获取流对应的字符串 
//        /// </summary>
//        /// <param name="s"></param>
//        /// <returns></returns>
//        public static string GetStreamValue(Stream s)
//        {
//            s.Position = 0L;
//            byte[] buffer = new byte[s.Length];
//            s.Read(buffer, 0, (int)s.Length);
//            s.Position = 0L;
//            return "";//EncodingUtility.ToASCIIStringFromBytes(buffer);
//        }

//        /// <summary>
//        /// 判断是否全路径（如果包含冒号，则认为是全路径） 
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public static bool IsUNCPath(string path)
//        {
//            if (path == null)
//            {
//                throw new ArgumentNullException("path");
//            }
//            return (path.IndexOf(":") != -1);
//        }

//        /// <summary>
//        /// 读取文件中的内容，以UTF8方式读取 
//        /// </summary>
//        /// <param name="file"></param>
//        /// <returns></returns>
//        public static string ReadFile(FileInfo file)
//        {
//            return ReadFile(file.FullName);
//        }

//        /// <summary>
//        /// 读取文件中的内容，以UTF8方式读取 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string ReadFile(string fullFileName)
//        {
//            using (StreamReader reader = new StreamReader(fullFileName, Encoding.UTF8))
//            {
//                return reader.ReadToEnd();
//            }
//        }

//        /// <summary>
//        /// 读取文件中的内容，可以指定编码格式  
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <param name="fileEncoding"></param>
//        /// <returns></returns>
//        public static string ReadFile(string fullFileName, Encoding fileEncoding)
//        {
//            using (StreamReader reader = new StreamReader(fullFileName, fileEncoding))
//            {
//                return reader.ReadToEnd();
//            }
//        }

//        /// <summary>
//        /// 读取文件中的内容，以gb2312方式读取 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string ReadGB2312File(string fullFileName)
//        {
//            using (StreamReader reader = new StreamReader(fullFileName, Encoding.GetEncoding("gb2312")))
//            {
//                return reader.ReadToEnd();
//            }
//        }

//        /// <summary>
//        /// 读取文件中的内容，以Unicode方式读取 
//        /// </summary>
//        /// <param name="fullFileName"></param>
//        /// <returns></returns>
//        public static string ReadUnicodeFile(string fullFileName)
//        {
//            using (StreamReader reader = new StreamReader(fullFileName, Encoding.Unicode))
//            {
//                return reader.ReadToEnd();
//            }
//        }

//        /// <summary>
//        /// 重命名文件
//        /// </summary>
//        /// <param name="rawFullFileName"></param>
//        /// <param name="newFullFileName"></param>
//        public static void ReNameFile(string rawFullFileName, string newFullFileName)
//        {
//            File.Move(rawFullFileName, newFullFileName);
//        }

//        /// <summary>
//        /// 保存二进制数组到物理文件 
//        /// </summary>
//        /// <param name="fileBytes"></param>
//        /// <param name="fileName"></param>
//        public static void SaveBitFile(byte[] fileBytes, string fileName)
//        {
//            if (fileBytes == null)
//            {
//                throw new ArgumentNullException("fileBytes");
//            }
//            CreatePath(GetFilePath(fileName));
//            FileStream stream = null;
//            try
//            {
//                stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
//                int length = fileBytes.Length;
//                stream.Write(fileBytes, 0, length);
//            }
//            finally
//            {
//                stream.Close();
//            }
//        }

//        /// <summary>
//        /// 设置文件是否隐藏 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="hidden"></param>
//        public static void SetFileHidden(string fileName, bool hidden)
//        {
//            if (ExistsFile(fileName))
//            {
//                FileAttributes fileAttributes = File.GetAttributes(fileName);
//                if (hidden)
//                {
//                    fileAttributes |= FileAttributes.Hidden;
//                    File.SetAttributes(fileName, fileAttributes);
//                }
//                else if ((fileAttributes | FileAttributes.Hidden) != fileAttributes)
//                {
//                    fileAttributes ^= FileAttributes.Hidden;
//                    File.SetAttributes(fileName, fileAttributes);
//                }
//            }
//        }

//        /// <summary>
//        /// 设置路径是否隐藏 
//        /// </summary>
//        /// <param name="path"></param>
//        /// <param name="hidden"></param>
//        public static void SetPathHidden(string path, bool hidden)
//        {
//            if (!ExistsPath(path))
//            {
//                CreatePath(path);
//            }
//            DirectoryInfo info = new DirectoryInfo(path);
//            FileAttributes fileAttributes = info.Attributes;
//            if (hidden)
//            {
//                fileAttributes |= FileAttributes.Hidden;
//                File.SetAttributes(path, fileAttributes);
//            }
//            else if ((fileAttributes | FileAttributes.Hidden) != fileAttributes)
//            {
//                fileAttributes ^= FileAttributes.Hidden;
//                File.SetAttributes(path, fileAttributes);
//            }
//        }

//        /// <summary>
//        /// 设置文件的只读属性,readOnly来标记如何设置readOnly 
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="readOnly"></param>
//        public static void SetReadOnly(string fileName, bool readOnly)
//        {
//            if (ExistsFile(fileName))
//            {
//                FileAttributes fileAttributes = File.GetAttributes(fileName);
//                if (readOnly)
//                {
//                    if ((fileAttributes | FileAttributes.ReadOnly) != fileAttributes)
//                    {
//                        fileAttributes |= FileAttributes.ReadOnly;
//                        File.SetAttributes(fileName, fileAttributes);
//                    }
//                }
//                else if ((fileAttributes | FileAttributes.ReadOnly) == fileAttributes)
//                {
//                    fileAttributes ^= FileAttributes.ReadOnly;
//                    File.SetAttributes(fileName, fileAttributes);
//                }
//            }
//        }

//        /// <summary>
//        /// 把数据表的内容写入文件 
//        /// </summary>
//        /// <param name="dt"></param>
//        public static void Write2File(DataTable dt)
//        {
//            DataSet set = new DataSet("ENTERPRISE");
//            DataTable table = dt.Copy();
//            set.Tables.Add(table);
//            Write2File(set.GetXml());
//        }

//        /// <summary>
//        /// 输出调试信息  
//        /// </summary>
//        /// <param name="result"></param>
//        public static void Write2File(object result)
//        {
//            if (result == null)
//            {
//                result = "null";
//            }
//            StreamWriter writer = null;
//            try
//            {
//                writer = new StreamWriter(DefaultLogPathFile, true);
//                writer.WriteLine(DateTime.Now.ToString() + " :  " + result.ToString());
//            }
//            finally
//            {
//                if (writer != null)
//                {
//                    writer.Flush();
//                    writer.Close();
//                }
//            }
//        }

//        /// <summary>
//        /// 系统当前路径
//        /// </summary>
//        public static string CurrentPath
//        {
//            get
//            {
//                if (fCurrentPath == string.Empty)
//                {
//                    fCurrentPath = FormatPath(ConfigUtility.GetWebOrAppConfigPath());
//                }
//                return fCurrentPath;
//            }
//        }

//        /// <summary>
//        /// 日志默认文件
//        /// </summary>
//        private static string DefaultLogPathFile
//        {
//            get
//            {
//                string fileName = ConfigUtility.GetConfigValue("DefaultLogPathFile", "默认日志文件", true);
//                if (fileName.Trim().Length < 1)
//                {
//                    fileName = "c:/debug.txt";
//                }

//                return fileName;
//            }
//        }
//    }
//}
