using System.Data.SqlClient;

namespace LearningVocabulary.Util
{
    class DBUtils
    {
        public static SqlConnection GetConnection()
        {
            string datasource = ".";
            string database = "VocabularyEnglish";
            return DBSqlServerUtils.GetDbSqlConnection(datasource, database);
        }
    }
}
