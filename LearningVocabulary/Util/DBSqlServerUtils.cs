using System.Data.SqlClient;

namespace LearningVocabulary.Util
{
    class DBSqlServerUtils
    {
        public static SqlConnection GetDbSqlConnection(string datasource, string database)
        {
            //Data Source = .; Initial Catalog = VocabularyEnglish; Integrated Security = True  --String connect sql
            string connect = @"Data Source = " + datasource + "; Initial Catalog = " + database +
                            "; Integrated Security = True";
            return new SqlConnection(connect);
        }
    }
}
