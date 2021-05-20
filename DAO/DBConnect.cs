using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class DBConnect
    {
        //// 147.121.73.252 (Main Server)
        //protected MySqlConnection _conn = new MySqlConnection("server=147.121.73.252;user=production;pwd=PDELS&Auto@{2020};database=tan_test;Connect Timeout=1000;charset=utf8;convertzerodatetime=true;");

        //// 147.121.73.3 (New Server)
        //protected MySqlConnection _conn = new MySqlConnection("server=147.121.73.252;user=automation;pwd=ELS&Automation@123;database=avery;Connect Timeout=10000;charset=utf8;convertzerodatetime=true;");

        // 147.121.59.138 (Main Server)
        protected MySqlConnection _conn = new MySqlConnection("server=147.121.59.138;user=planning;pwd=PELS&Auto@{2020};database=avery;Connect Timeout=10000;charset=utf8;convertzerodatetime=true;");

        //// 147.121.59.138 (TEST Server)
        //protected MySqlConnection _conn = new MySqlConnection("server=147.121.59.138;user=production;pwd=PDELS&Auto@{2020};database=ztan_test;Connect Timeout=10000;charset=utf8;convertzerodatetime=true;");

    }
}