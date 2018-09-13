using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace Splendor
{
    /// <summary>
    /// contains methods and attributes to connect and deal with the database
    /// TO DO : le modèle de données n'est pas super, à revoir!!!!
    /// </summary>
    class ConnectionDB
    {

        //connection to the database
        private SQLiteConnection m_dbConnection;

        /// <summary>
        /// if File exist return false
        /// </summary>
        /// <param name="fileName">string file</param>
        /// <returns></returns>
        public bool Exist(string fileName)
        {
            return System.IO.File.Exists(fileName);
        }



        /// <summary>
        /// constructor : creates the connection to the database SQLite
        /// </summary>
        public ConnectionDB()
        {

            //if BDD exist do not create Table and insert data
            if (Exist("Splendor.sqlite"))
            {
                m_dbConnection = new SQLiteConnection("Data Source=Splendor.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            else
            {
                SQLiteConnection.CreateFile("Splendor.sqlite");

                m_dbConnection = new SQLiteConnection("Data Source=Splendor.sqlite;Version=3;");
                m_dbConnection.Open();

                //create and insert players
                CreateInsertPlayer();
                //Create and insert cards
                //TO DO
                CreateInsertCards();
                //Create and insert ressources
                CreateInsertRessources();
                //TO DO

                //Create and insert Cost
                CreateInsertCost();

                //Create and insert nbCoin
                CreateInsertNbCoin();

                

            }


        }




        /// <summary>
        /// Exectute SQL request
        /// </summary>
        /// <param name="sql"> SQL request</param>
        public void DoSqlRequest(string sql)
        {

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }


        /// <summary>
        /// get the list of cards according to the level
        /// </summary>
        /// <returns>cards stack</returns>
        public Stack<Card> GetListCardAccordingToLevel(int level)
        {
            //Get all the data from card table selecting them according to the data
            //TO DO
            //Create an object "Stack of Card"
            Stack<Card> listCard = new Stack<Card>();
            //do while to go to every record of the card table
            //while (....)
            //{
            //Get the ressourceid and the number of prestige points
            //Create a card object

            //select the cost of the card : look at the cost table (and other)

            //do while to go to every record of the card table
            //while (....)
            //{
            //get the nbRessource of the cost
            //}
            //push card into the stack

            //}
            return listCard;
        }


        /// <summary>
        /// create the "player" table and insert data
        /// </summary>
        private void CreateInsertPlayer()
        {
            DoSqlRequest("CREATE TABLE player (idPlayer INT PRIMARY KEY, pseudo VARCHAR(20))");
            DoSqlRequest("insert into player(idPlayer, pseudo) values(0, 'Fred')");
            DoSqlRequest("insert into player (idPlayer, pseudo) values (1, 'Harry')");
            DoSqlRequest("insert into player (idPlayer, pseudo) values (2, 'Sam')");
        }




        /// <summary>
        /// get the name of the player according to his id
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns></returns>
        public string GetPlayerName(int id)
        {
            string sql = "select pseudo from player where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader["pseudo"].ToString();
            }
            return name;
        }

        /// <summary>
        /// create the table "ressources" and insert data
        /// </summary>
        private void CreateInsertRessources()
        {
            //Crate Table
            DoSqlRequest("CREATE TABLE Ressource (idRessource INT PRIMARY KEY, name varchar(20))");
            DoSqlRequest("Insert Into Ressource (idRessource, name) values(1, 'Ruby')");
            DoSqlRequest("Insert Into Ressource (idRessource, name) values(2, 'Emeraude')");
            DoSqlRequest("Insert Into Ressource (idRessource, name) values(3, 'Onyx')");
            DoSqlRequest("Insert Into Ressource (idRessource, name) values(4, 'Saphir')");
            DoSqlRequest("Insert Into Ressource (idRessource, name) values(5, 'Diamant')");
        }



        /// <summary>
        ///  create tables "cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        {
            //Create Table
            DoSqlRequest("CREATE TABLE Card (idCard INTEGER PRIMARY KEY AUTOINCREMENT,fkRessource INT, level INT, nbPtPrestige INT, fkPlayer INT,FOREIGN KEY(fkRessource) REFERENCES Ressource(idRessource), FOREIGN KEY(fkPlayer) REFERENCES Player(idPlayer))");

            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (2,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (3,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (4,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (5,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (6,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (7,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (8,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (9,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (10,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (11,0,4,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (12, 4,3,5)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (13, 3,3,5)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (14, 2,3,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (15, 5,3,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (16, 1,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (17, 2,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (18, 5,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (19, 5,3,5)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (20, 1,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (21, 4,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (22, 2,3,5)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (23, 3,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (24, 1,3,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (25, 4,3,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (26, 2,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (27, 3,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (28, 4,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (29, 1,3,5)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (30, 5,3,4)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (31, 3,3,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (32, 5,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (33, 1,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (34, 5,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (35, 5,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (36, 5,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (37, 2,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (38, 4,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (39, 4,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (40, 2,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (41, 2,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (42, 3,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (43, 1,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (44, 5,2,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (45, 4,2,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (46, 2,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (47, 3,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (48, 1,2,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (49, 4,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (50, 3,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (51, 2,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (52, 4,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (53, 1,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (54, 1,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (55, 3,2,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (56, 4,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (57, 3,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (58, 1,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (59, 5,2,2)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (60, 2,2,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (61, 3,2,3)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (62, 3,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (63, 2,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (64, 1,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (65, 5,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (66, 4,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (67, 5,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (68, 5,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (69, 5,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (70, 5,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (71, 5,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (72, 5,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (73, 5,1,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (74, 1,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (75, 1,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (76, 1,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (77, 1,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (78, 1,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (79, 1,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (80, 1,1,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (81, 3,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (82, 3,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (83, 3,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (84, 3,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (85, 3,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (86, 3,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (87, 3,1,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (88, 4,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (89, 4,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (90, 4,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (91, 4,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (92, 4,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (93, 4,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (94, 4,1,1)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (95, 2,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (96, 2,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (97, 2,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (98, 2,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (99, 2,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (100, 2,1,0)");
            DoSqlRequest("insert into card(idcard, fkRessource, level, nbPtPrestige) values (101, 2,1,1)");




        }


        private void CreateInsertNbCoin()
        {
            DoSqlRequest("Create Table NbCoin (idNbCoin INTEGER PRIMARY KEY AUTOINCREMENT, fkPlayer INT, fkRessource INT, nbCoin INT, FOREIGN KEY(fkPlayer) References Player(idPlayer), FOREIGN KEY(fkRessource) References Ressources(idRessource))");
        }

        private void CreateInsertCost()
        {
            DoSqlRequest("CREATE TABLE Cost (idCost INTEGER PRIMARY KEY AUTOINCREMENT, fkCard INT, fkRessource INT, nbRessource INT, FOREIGN KEY(fkCard) References Card(idCard), FOREIGN KEY fkRessource References Ressource(idRessource))");


        }

    }
}
