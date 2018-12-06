using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LearningVocabulary.Entity;
using LearningVocabulary.Util;
using Microsoft.Win32;

namespace LearningVocabulary
{
    public partial class FormMain : Form
    {
        private static string TAG = "FormMain: ";
        private SqlConnection conn;
        private SqlCommand cmd;
        private List<Vocabulary> _vocabularies;
        private static string NAMEAPP = "Learning Vocabulary";

        public FormMain()
        {
            InitializeComponent();

            ConnectToSQL();
        }

        private void ConnectToSQL()
        {
            if (conn == null)
            {
                conn = DBUtils.GetConnection();
            }

            try
            {
                conn.Open();
                WriteLog.WriteLogSuccess(TAG, "ConnectToSQL()", "Connect sql success");
                _vocabularies = new List<Vocabulary>();

                QueryGetVocabulary();
            }
            catch (Exception e)
            {
                WriteLog.WriteLogException(TAG, "ConnectToSQL(5)", e.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private void QueryGetVocabulary()
        {
            int index = getNumberOfIndex() * 5 + 1;
            String query = "select stt, vocabulary, spelling, means from Cntt where stt between " + index + " and " + (index + 4);
            Console.WriteLine("Query: " + query);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            _vocabularies.Clear();
            using (DbDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Vocabulary tVocabulary = new Vocabulary();

                        tVocabulary.stt = Convert.ToInt16(dataReader.GetValue(0));
                        tVocabulary.vocabulary = dataReader.GetString(1);
                        if (!dataReader.IsDBNull(2))
                        {
                            tVocabulary.spelling = dataReader.GetString(2);
                        }
                        else
                        {
                            tVocabulary.spelling = "";
                        }


                        tVocabulary.means = dataReader.GetString(3);


                        _vocabularies.Add(tVocabulary);
                    }
                }
            }

            DisplayVocabulary();
        }

        private void DisplayVocabulary()
        {
            for (int i = 0; i < _vocabularies.Count; i++)
            {
                WriteLog.WriteLogSuccess(TAG, "DisplayVocabulary()", _vocabularies.ElementAt(i).stt + " - " + _vocabularies.ElementAt(i).vocabulary);
            }
            DrawImage.Draw(_vocabularies);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (IsStartupItem())
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue(NAMEAPP, Application.ExecutablePath.ToString());
                    MessageBox.Show("Bật khởi động cùng máy thành công", "Settup", MessageBoxButtons.OK);
                }
            }
            else MessageBox.Show("Chương trình đã được bật khởi động cùng hệ thống", "Error", MessageBoxButtons.OK);

        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (IsStartupItem())
            {
                rkApp.DeleteValue(NAMEAPP, false);
                MessageBox.Show("Tắt khởi động cùng máy thành công", "Settup", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Chương trình chưa được cài đặt chạy cùng hệ thống", "Error", MessageBoxButtons.OK);
        }

        private bool IsStartupItem()
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp.GetValue(NAMEAPP) == null)
                return false;
            else
                return true;
        }

        private int getNumberOfIndex()
        {
            int day = DateTime.UtcNow.Day - 1;
            if (day < 0 || day > 30)
            {
                return 0;
            }
            return day;
        }
    }
}
