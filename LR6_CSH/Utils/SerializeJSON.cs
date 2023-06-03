//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Windows.Forms;
//using Newtonsoft.Json;

//namespace LR4_CSH
//{
//    static class SerializeJSON
//    {
//        public static void SerializeStudents(string path)
//        {
//            if (string.IsNullOrEmpty(path)) return;
//            try
//            {
//                var jsonSerializer = new JsonSerializer();
//                using (var file = new StreamWriter(path))
//                {
//                    jsonSerializer.Serialize(file, Group.Students);
//                }
//            }
//            catch (Exception ex)
//            {
//                Form1 owner = new Form1();
//                MessageBox.Show(owner, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }
//        public static void DeserializeStudents(string filePath)
//        {
//            if (string.IsNullOrEmpty(filePath)) return;
//            try
//            {
//                var jsonDeserializer = new JsonSerializer();
//                using (var file = new StreamReader(filePath))
//                {
//                    Group.Students = (List<Student>)jsonDeserializer.Deserialize(file, typeof(List<Student>));
//                }
//            }
//            catch (Exception ex)
//            {
//                Form1 owner = new Form1();
//                MessageBox.Show(owner, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }
//    }
//}
