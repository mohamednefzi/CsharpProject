using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALConnection
{
   public  class Connection
    {
        private static string connStr = @"Data Source=VIEWW7-2016-60\SQLEXPRESS;Initial Catalog=MyTest;Integrated Security=True;Connect Timeout=30";


        /// <summary>
        /// inserer une ligne et retourner la clé généré par la requette
        /// </summary>
        /// <param name="requette"></param>
        /// <param name="listeParams"></param>
        /// <returns></returns>
       public static int Insert(string requette, List<SqlParameter> listeParams)
        {
            int idGenerated = -2;
            using (SqlConnection conn = new SqlConnection(connectionString: connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = requette;
                    foreach (SqlParameter param in listeParams)
                    {
                        cmd.Parameters.Add(param);
                    }
                    idGenerated = (int)cmd.ExecuteScalar();
                    Console.WriteLine("auto-incremented id: {0}", idGenerated);
                }
            }
            return idGenerated;
        }


        /// <summary>
        /// modifier des lignes et retourner le nombre de lignes modifiées
        /// </summary>
        /// <param name="requette"></param>
        /// <param name="listeParams"></param>
      public  static int Update(string requette, List<SqlParameter> listeParams)
        {
            int nbLignes = -1;
            using (SqlConnection conn = new SqlConnection(connectionString: connStr))

            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = requette;
                    foreach (SqlParameter param in listeParams)
                    {
                        cmd.Parameters.Add(param);
                    }
                    nbLignes = cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("updated");
            return nbLignes;

        }



        /// <summary>
        /// supprimer des lignes et retourner le nombre de ligne supprimer
        /// </summary>
        /// <param name="requette"></param>
        /// <param name="listeParams"></param>
        /// <returns></returns>
      public  static int Delete(string requette, List<SqlParameter> listeParams)
        {
            int nbLignes = -1;
            using (SqlConnection conn = new SqlConnection(connectionString: connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = requette;
                    foreach (SqlParameter param in listeParams)
                    {
                        cmd.Parameters.Add(param);
                    }
                    nbLignes = cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("deleted");
            return nbLignes;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requette"></param>
        /// <param name="listeParams"></param>
        /// <returns></returns>
       public static DataSet selectQuery(string requette, List<SqlParameter> listeParams)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = requette;
                    foreach (SqlParameter param in listeParams)
                    {
                        cmd.Parameters.Add(param);
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                }
                return dataSet;
            }

        }
    }

}
