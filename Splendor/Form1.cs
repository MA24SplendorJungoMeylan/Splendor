/**
 * \file      frmAddVideoGames.cs
 * \author    Jeremy Jungo, Benoit Meylan
 * \version   0.1
 * \date      August 22. 2018
 * \brief     Form to play.
 *
 * \details   This form enables to choose coins or cards to get ressources (precious stones) and prestige points 
 * to add and to play with other players
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Splendor
{
    /// <summary>
    /// manages the form that enables to play with the Splendor
    /// </summary>
    public partial class frmSplendor : Form
    {
        //used to store the number of coins selected for the current round of game


        private int[] nbCoinsSelected = new int[] { 0, 0, 0, 0, 0 };

        const int maxCoins = 7;

        private int[] availableCoins = new int[] { maxCoins, maxCoins, maxCoins, maxCoins, maxCoins };

        //used for the three differents coins
        private int threeCoins = 0;

        //id of the player that is playing
        private int currentPlayerId = 0;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn = new ConnectionDB();

        //list of players
        private List<Player> Players = new List<Player>();

        //lists of cards 
        Stack<Card> listCardOne = new Stack<Card>();
        Stack<Card> listCardTwo = new Stack<Card>();
        Stack<Card> listCardThree = new Stack<Card>();


        /// <summary>
        /// constructor
        /// </summary>
        public frmSplendor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads the form and initialize data in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_Load(object sender, EventArgs e)
        {
            lblGoldCoin.Text = "5";

            lblDiamandCoin.Text = availableCoins[4].ToString();
            lblEmeraudeCoin.Text = availableCoins[3].ToString();
            lblOnyxCoin.Text = availableCoins[2].ToString();
            lblRubisCoin.Text = availableCoins[1].ToString();
            lblSaphirCoin.Text = availableCoins[0].ToString();

            //loads cards from database
            listCardOne = conn.GetListCardAccordingToLevel(1);
            listCardTwo = conn.GetListCardAccordingToLevel(2);
            listCardThree = conn.GetListCardAccordingToLevel(3);

            //fill the cards into interface
            fillCards();

            //fin TO DO
            this.Width = 680;
            this.Height = 540;

            enableClicLabel = false;

            lblChoiceDiamand.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceEmeraude.Visible = false;
            cmdValidateChoice.Visible = false;
            cmdResetChoice.Visible = false;
            cmdNextPlayer.Visible = false;

            //we wire the click on all cards to the same event
            //TO DO for all cards
            txtLevel11.Click += ClickOnCard;
            txtLevel12.Click += ClickOnCard;
            txtLevel13.Click += ClickOnCard;
            txtLevel14.Click += ClickOnCard;

            txtLevel21.Click += ClickOnCard;
            txtLevel22.Click += ClickOnCard;
            txtLevel23.Click += ClickOnCard;
            txtLevel24.Click += ClickOnCard;

            txtLevel31.Click += ClickOnCard;
            txtLevel32.Click += ClickOnCard;
            txtLevel33.Click += ClickOnCard;
            txtLevel34.Click += ClickOnCard;

            txtNoble1.Click += ClickOnCard;
            txtNoble2.Click += ClickOnCard;
            txtNoble3.Click += ClickOnCard;
            txtNoble4.Click += ClickOnCard;
        }

        /// <summary>
        /// Fill the cards into the interface
        /// </summary>
        public void fillCards()
        {           
            int nbDataInStack = listCardOne.Count;
            int i = 0;
            foreach (Control ctrl in flwCardLevel1.Controls)
            {
                if (i < nbDataInStack && ctrl.Text == "")
                {
                    ctrl.Text = listCardOne.Pop().ToString();
                    i++;
                }

            }
            
            nbDataInStack = listCardTwo.Count;
            i = 0;
            foreach (Control ctrl in flwCardLevel2.Controls)
            {
                if (i < nbDataInStack && ctrl.Text == "")
                {
                    ctrl.Text = listCardTwo.Pop().ToString();
                    i++;
                }

            }
            
            nbDataInStack = listCardThree.Count;
            i = 0;
            foreach (Control ctrl in flwCardLevel3.Controls)
            {
                if (i < nbDataInStack && ctrl.Text == "")
                {
                    ctrl.Text = listCardThree.Pop().ToString();
                    i++;
                }

            }
        }

        /// <summary>
        /// Separates the elements of one card and check if the player can buy the card. 
        /// </summary>
        /// <param name="sender"> textbox of the card</param>
        /// <param name="e"></param>
        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
            TextBox txtBox = sender as TextBox;

            MessageBox.Show(txtBox.Text);


            int[] coutCarte = new int[5]; //tableau qui répertorie le cout d'une carte
            int nbCoinsLost = 0;
            int coinsNeed = 0;
            bool canBuy = true;
            string[] informations = txtBox.Text.Split('\t');//information de la carte
            string ressource = informations[0]; //ressource de la carte
            string ptPrestigeString = informations[1]; //pt de presstige de la carte
            string coutString = informations[2]; // cout d'une carte sous forme de string
            
            string pattern = @"[^0-9a-zA-Z\n]";

            Regex rex = new Regex(pattern); //créer un nouveau paterne 

            string result = rex.Replace(coutString, ""); //remplace toutes les occurence qui ne sont pas dans le paterne

            string[] couts = result.Split('\n'); //tableau des couts

            
            //décompose le cout d'une carte pour l'inscrire dans un tableau
            for(int i = 0; i < couts.Length; i++)
            {
                string res = couts[i];
                int ressQuantity = 0;
                int resLenght = res.Length;
                if(resLenght > 0)
                {
                    string ressType = res.Substring(0, resLenght-1);
                   
                    ressQuantity = System.Convert.ToInt32(res.Substring(resLenght-1));
                  
                    switch (ressType)
                    {
                        case "Rubis":
                            coutCarte[0] = ressQuantity;
                            break;
                        case "Saphir":
                            coutCarte[1] = ressQuantity;
                            break;
                        case "Onyx":
                            coutCarte[2] = ressQuantity;
                            break;
                        case "Emeraude":
                            coutCarte[3] = ressQuantity;
                            break;
                        case "Diamand":
                            coutCarte[4] = ressQuantity;
                            break;
                    }
                }

            }

            
            //Est-ce que le joueur à assez de ressource pour acheter la carte
            for (int i = 0; i < coutCarte.Length; i++)
            {
                coinsNeed = coutCarte[i];
                coinsNeed -= Players[currentPlayerId].Ressources[i];
                coinsNeed -= Players[currentPlayerId].Coins[i];

                //le joueur n'a pas assez de ressources pour acheter la carte
                if(coinsNeed > 0)
                { 
                    canBuy = false;
                    break;
                }
            }

            //le joueur a assez de ressources
            if(canBuy)
            {
                //remove coins from player inventory
                for (int i = 0; i < coutCarte.Length; i++)
                {
                    nbCoinsLost = (coutCarte[i] - Players[currentPlayerId].Ressources[i]);
                    Players[currentPlayerId].Coins[i] -= nbCoinsLost;
                    availableCoins[i] += nbCoinsLost;
                    conn.BackPlayerCoins(currentPlayerId, i, -1*nbCoinsLost);

                   
                }

                switch (ressource)
                {
                    case "Rubis":
                        Players[currentPlayerId].Ressources[0] += 1;
                        break;
                    case "Saphir":
                        Players[currentPlayerId].Ressources[1] += 1;
                        break;
                    case "Onyx":
                        Players[currentPlayerId].Ressources[2] += 1;
                        break;
                    case "Emeraude":
                        Players[currentPlayerId].Ressources[3] += 1;
                        break;
                    case "Diamand":
                        Players[currentPlayerId].Ressources[4] += 1;
                        break;
                }

               

                //ajoute les pts de prestige de la carte au joueur
                try
                {
                    int ptPrestige = System.Convert.ToInt32(ptPrestigeString);
                    Players[currentPlayerId].PtPrestige += ptPrestige; 


                    //a player win
                    if(ptPrestige >= 15)
                    {
                        MessageBox.Show(Players[currentPlayerId].Name + " a gagné");

                    }
                }
                catch
                {

                }
                
                //empty the txtBox
                txtBox.Text = "";
                fillCards();
            }
            else
            {
                MessageBox.Show("Vous n'avez pas assez pour cette carte");
            }

            updateScreen();
        }

        /// <summary>
        /// Update the ressources textBox of a player
        /// </summary>
      

        /// <summary>
        /// the player click on the "Play" button and start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.Width = 680;
            this.Height = 780;

            int id = 0;

            for (int i = 0; i < 3;i++)
            {
                id = i;
                LoadPlayer(id);
            }

            //Display the coins of player
            lblPlayerDiamandCoin.Text = Players[currentPlayerId].Coins[4].ToString();
            lblPlayerOnyxCoin.Text = Players[currentPlayerId].Coins[2].ToString();
            lblPlayerRubisCoin.Text = Players[currentPlayerId].Coins[0].ToString();
            lblPlayerSaphirCoin.Text = Players[currentPlayerId].Coins[3].ToString();
            lblPlayerEmeraudeCoin.Text = Players[currentPlayerId].Coins[1].ToString();

        }


        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) { 

            enableClicLabel = true;

            string name = conn.GetPlayerName(id);

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = "";
            lblChoiceOnyx.Text = "";
            lblChoiceRubis.Text = "";
            lblChoiceSaphir.Text = "";
            lblChoiceEmeraude.Text = "";

            lblChoiceCard.Text = "";

            Player player = new Player();
            player.Name = name;
            player.Id = id;
            player.Ressources = new int[]{0,0,0,0,0};
            player.Coins = conn.GetPlayerCoins(id); //Coins player 

            lblPlayer.Text = "Jeu de " + name;

            cmdPlay.Enabled = false;

            Players.Add(player);
        }

        /// <summary>
        /// click on the red coin (rubis) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRubisCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                lblClick(ref nbCoinsSelected[0], ref availableCoins[0], ref lblRubisCoin, ref lblChoiceRubis);
            }
        }

        /// <summary>
        /// click on the blue coin (saphir) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSaphirCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                lblClick(ref nbCoinsSelected[1], ref availableCoins[1], ref lblSaphirCoin, ref lblChoiceSaphir);
            }
        }

        /// <summary>
        /// click on the black coin (onyx) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblOnyxCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                lblClick(ref nbCoinsSelected[2], ref availableCoins[2], ref lblOnyxCoin, ref lblChoiceOnyx);
            }
        }

        /// <summary>
        /// click on the green coin (emeraude) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmeraudeCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                lblClick(ref nbCoinsSelected[3], ref availableCoins[3], ref lblEmeraudeCoin, ref lblChoiceEmeraude);
            }
        }

        /// <summary>
        /// click on the white coin (diamand) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDiamandCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                lblClick(ref nbCoinsSelected[4], ref availableCoins[4], ref lblDiamandCoin, ref lblChoiceDiamand);
            }
        }

        /// <summary>
        /// Check if the player can take an other coins
        /// </summary>
        /// <param name="NbCoins">nb coins selected by the player </param>
        /// <param name="availableStone">nb of coins are already available</param>
        /// <param name="lblstone">label of a ressource</param>
        /// <param name="lblChoice"></param>
        public void lblClick(ref int NbCoins, ref int availableStone,ref Label lblstone, ref Label lblChoice)
        {
            
            if (nbCoinsSelected[0] == 2 || nbCoinsSelected[1] == 2 || nbCoinsSelected[2] == 2 || nbCoinsSelected[3] == 2 || nbCoinsSelected[4] == 2)
            {
                MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
            }
            else
            {

                threeCoins = nbCoinsSelected[0] + nbCoinsSelected[1] + nbCoinsSelected[2] + nbCoinsSelected[3] + nbCoinsSelected[4];
                if (NbCoins == 1 && threeCoins == 2)
                {
                    MessageBox.Show("impossible de prendre 1 jeton en plus");
                }
                else
                {
                    if (threeCoins == 3)
                    {
                        MessageBox.Show("On peut retirer seulement 3 pierres différentes");
                    }
                    else
                    {
                        cmdValidateChoice.Visible = true;
                        cmdResetChoice.Visible = true;
                        lblChoice.Visible = true;
                        if (availableStone== 2)
                        {
                            MessageBox.Show("Le stock ne peut pas descendre en dessous de 2");
                        }
                        else
                        {
                            if (NbCoins == 2)
                            {
                                MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                            }
                            else
                            {
                                availableStone--;
                                NbCoins++;
                                lblstone.Text = availableStone.ToString();
                                lblChoice.Text = NbCoins + "\r\n";
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidateChoice_Click(object sender, EventArgs e)
        {
            //PlayerCoins var for Show and Back to BD
            int PlayerRubisCoin = 0;
            int PlayerSaphirCoin = 0;
            int PlayerOnyxCoin = 0;
            int PlayerEmeraudeCoin = 0;
            int PlayerDiamandCoin = 0;


            int[] nbCoins = { nbCoinsSelected[0], nbCoinsSelected[1], nbCoinsSelected[2], nbCoinsSelected[3], nbCoinsSelected[4] };
            
            //int TotPlayerCoins = 0;
            //Instentiation Player

            Players[currentPlayerId].Coins = conn.GetPlayerCoins(currentPlayerId); //get Player coins from BD
            PlayerRubisCoin = Players[currentPlayerId].Coins[0] + nbCoins[0];
            PlayerSaphirCoin = Players[currentPlayerId].Coins[1] + nbCoins[1];
            PlayerOnyxCoin = Players[currentPlayerId].Coins[2] + nbCoins[2];
            PlayerEmeraudeCoin = Players[currentPlayerId].Coins[3] + nbCoins[3];            
            PlayerDiamandCoin = Players[currentPlayerId].Coins[4] + nbCoins[4];

            int[] playerCoins = { PlayerRubisCoin, PlayerSaphirCoin, PlayerOnyxCoin, PlayerEmeraudeCoin, PlayerDiamandCoin };
            
            //add coins selected by current player to DB
            for(int i = 0; i < playerCoins.Length; i++)
            {
                if(nbCoins[i] > 0) //add only coins who are upper than 0
                {
                    conn.BackPlayerCoins(currentPlayerId, i, nbCoins[i]);
                }    
            }

            Players[currentPlayerId].Coins = playerCoins;
 
            updateScreen();
            /*TotPlayerCoins = Convert.ToInt32(lblPlayerRubisCoin.Text) + 9;
            //10 coins max player
           if (TotPlayerCoins == 10)
           {
               MessageBox.Show("Impossible de posséder plus de pièces");
           }
           else
           {
               //Back PlayerCoins to BD ------------------------------------------------------------------------------------------------
               conn.BackPlayerCoins(id);
                //Next Player
               cmdNextPlayer.Visible = true;
           }*/
            //10 coins max player

            cmdValidateChoice.Enabled = false;
            cmdResetChoice.Enabled = false;
            cmdNextPlayer.Enabled = true;

            cmdNextPlayer.Visible = true;
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertPlayer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A implémenter !");
        }

        /// <summary>
        /// Pass to the next player show his inventory and his cards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button
            updateScreen();
            resetChoice();

            cmdResetChoice.Enabled = true;
            cmdValidateChoice.Enabled = true;
            cmdNextPlayer.Enabled = false;

            currentPlayerId = ((currentPlayerId + 1) % 3) ;

            lblPlayer.Text = "Jeu de " + Players[currentPlayerId].Name;
            Console.WriteLine("Joueur suivant, id joueur : " + Players[currentPlayerId].Id);

            updateScreen();

            //label choice invisible
            lblChoice();
        }

        /// <summary>
        /// update label on the screen
        /// </summary>
        public void updateScreen()
        {
            lblPlayerRubisCoin.Text = Players[currentPlayerId].Coins[0].ToString();
            lblPlayerSaphirCoin.Text = Players[currentPlayerId].Coins[1].ToString();
            lblPlayerOnyxCoin.Text = Players[currentPlayerId].Coins[2].ToString();
            lblPlayerEmeraudeCoin.Text = Players[currentPlayerId].Coins[3].ToString();
            lblPlayerDiamandCoin.Text = Players[currentPlayerId].Coins[4].ToString();

            lblRubisCoin.Text = availableCoins[0].ToString();
            lblSaphirCoin.Text = availableCoins[1].ToString();
            lblOnyxCoin.Text = availableCoins[2].ToString();
            lblEmeraudeCoin.Text = availableCoins[3].ToString();
            lblDiamandCoin.Text = availableCoins[4].ToString();

            lblChoiceRubis.Text = nbCoinsSelected[0].ToString();
            lblChoiceSaphir.Text = nbCoinsSelected[1].ToString();
            lblChoiceOnyx.Text = nbCoinsSelected[2].ToString();
            lblChoiceEmeraude.Text = nbCoinsSelected[3].ToString();
            lblChoiceDiamand.Text = nbCoinsSelected[4].ToString();

            txtPlayerRubisCard.Text = Players[currentPlayerId].Ressources[0].ToString();
            txtPlayerSaphirCard.Text = Players[currentPlayerId].Ressources[1].ToString();
            txtPlayerOnyxCard.Text = Players[currentPlayerId].Ressources[2].ToString();
            txtPlayerEmeraudeCard.Text = Players[currentPlayerId].Ressources[3].ToString();
            txtPlayerDiamandCard.Text = Players[currentPlayerId].Ressources[4].ToString();

            lblNbPtPrestige.Text = "Nb Pt Prestige : " + Players[currentPlayerId].PtPrestige;
        }

        private void lblChoiceRubis_Click(object sender, EventArgs e)
        {
            //Don't used
        }

        /// <summary>
        /// Reset the coins who choose the player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdResetChoice_Click(object sender, EventArgs e)
        {
            

            for (int i = 0; i < Enum.GetValues(typeof(Ressources)).Length; i++)
            {
                availableCoins[i] += nbCoinsSelected[0];
            }

            resetChoice();

            updateScreen();

            //Reset threeCoins
            threeCoins = 0;

            //label choice invisible
            lblChoice();

        }

        /// <summary>
        /// reset the inventory
        /// </summary>
        public void resetChoice()
        {
            for (int i = 0; i < Enum.GetValues(typeof(Ressources)).Length; i++)
            {
                nbCoinsSelected[i] = 0;
            }
          
        }

        /// <summary>
        /// Reset and invisible lblChoice of players
        /// </summary>
        public void lblChoice()
        {
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceEmeraude.Visible = false;
            lblChoiceDiamand.Visible = false;
        }

        /// <summary>
        /// when the player close the form reset all coins 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.deleteCoins();
        }
    }
}