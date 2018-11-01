﻿/**
 * \file      frmAddVideoGames.cs
 * \author    Jeremy Jungo, Benoit Meylan
 * \version   1.0
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

namespace Splendor
{
    /// <summary>
    /// manages the form that enables to play with the Splendor
    /// </summary>
    public partial class frmSplendor : Form
    {
        //used to store the number of coins selected for the current round of game
        private int nbRubis;
        private int nbSaphir;
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        

        private int aviableRubis = 7;
        private int aviableSaphir = 7;
        private int aviableOnyx = 7;
        private int aviableEmeraude= 7;
        private int aviableDiamand = 7;

        //used for the three differents coins
        private int threeCoins = 0;

        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn;

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

            lblRubisCoin.Text = aviableRubis.ToString();
            lblSaphirCoin.Text = aviableSaphir.ToString();
            lblOnyxCoin.Text = aviableOnyx.ToString();
            lblEmeraudeCoin.Text = aviableEmeraude.ToString();
            lblDiamandCoin.Text = aviableDiamand.ToString();

            conn = new ConnectionDB();

            //load cards from the database
            //they are not hard coded any more
            //TO DO

            Card card11 = new Card();
            card11.Level = 1;
            card11.PrestigePt = 1;
            card11.Cout = new int[] { 2, 0, 2, 0, 2 };
            card11.Ress = Ressources.Rubis;

            Card card12 = new Card();
            card12.Level = 1;
            card12.PrestigePt = 0;
            card12.Cout = new int[] { 0, 1, 2, 1, 0 };
            card12.Ress = Ressources.Saphir;

            txtLevel11.Text = card11.ToString();
            txtLevel12.Text = card12.ToString();

            //load cards from the database
            Stack<Card> listCardOne = conn.GetListCardAccordingToLevel(1);
            Stack<Card> listCardTwo = conn.GetListCardAccordingToLevel(2);
            Stack<Card> listCardThree = conn.GetListCardAccordingToLevel(3);



            //Go through the results
            //Don't forget to check when you are at the end of the stack
            
            //fin TO DO

            this.Width = 680;
            this.Height = 540;

            enableClicLabel = false;

            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceEmeraude.Visible = false;
            lblChoiceDiamand.Visible = false;

            cmdValidateChoice.Visible = false;
            cmdResetChoice.Visible = false;
            cmdNextPlayer.Visible = false;

            //we wire the click on all cards to the same event
            //TO DO for all cards
            txtLevel11.Click += ClickOnCard;
        }

        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
        }

        /// <summary>
        /// click on the play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.Width = 680;
            this.Height = 780;

            int id = 0;
           
            LoadPlayer(id);

        }


        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) { 

            enableClicLabel = true;

            string name = conn.GetPlayerName(currentPlayerId);


            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = "";
            lblChoiceOnyx.Text = "";
            lblChoiceRubis.Text = "";
            lblChoiceSaphir.Text = "";
            lblChoiceEmeraude.Text = "";

            lblChoiceCard.Text = "";

            //no coins selected
            nbDiamand = 0;
            nbOnyx = 0;
            nbRubis = 0;
            nbSaphir = 0;
            nbEmeraude = 0;

            Player player = new Player();
            player.Name = name;
            player.Id = id;
            player.Ressources = new int[] { 2, 0, 1, 1, 1 };
            player.Coins = conn.GetPlayerCoins(id); //Coins player

            lblPlayerRubisCoin.Text = player.Coins[0].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[3].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[2].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[1].ToString();
            lblPlayerDiamandCoin.Text = player.Coins[4].ToString();
            currentPlayerId = id;

            lblPlayer.Text = "Jeu de " + name;

            cmdPlay.Enabled = false;
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
                //Verifie si le nombre de jetons de chaque sorte font 2
                if (nbSaphir == 2 || nbOnyx == 2 || nbEmeraude == 2 || nbDiamand == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;
                    MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                }
                else
                {
                    // total de coins pris
                    threeCoins = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
                    if (nbRubis == 1 && threeCoins == 2)
                    {
                        lblRubisCoin.Enabled = false;
                        MessageBox.Show("impossible de prendre 1 rubis en plus");
                    }
                    else
                    {
                        if (threeCoins == 3)
                        {
                            lblRubisCoin.Enabled = false;
                            lblSaphirCoin.Enabled = false;
                            lblOnyxCoin.Enabled = false;
                            lblEmeraudeCoin.Enabled = false;
                            lblDiamandCoin.Enabled = false;
                            MessageBox.Show("Il est impossible de retirer plus de 3 pierres différentes");
                        }
                        else
                        {
                            cmdValidateChoice.Visible = true;
                            cmdResetChoice.Visible = true;
                            lblChoiceRubis.Visible = true;

                            if (aviableRubis == 2)
                            {
                                MessageBox.Show("Le stock ne peut pas descendre en dessous de 2");
                            }
                            else
                            {

                                if (nbRubis == 2)
                                {
                                    MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                                }
                                else
                                {
                                    aviableRubis--;
                                    nbRubis++;
                                    lblRubisCoin.Text = aviableRubis.ToString();
                                    lblChoiceRubis.Text = nbRubis + "\r\n";
                                }
                            }
                        }
                    }
                }
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
                if (nbRubis == 2 || nbOnyx == 2 || nbEmeraude == 2 || nbDiamand == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;
                    MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                }
                else
                {
                    // total de coins pris
                    threeCoins = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
                    if (nbRubis == 1 && threeCoins == 2)
                    {
                        lblRubisCoin.Enabled = false;
                        MessageBox.Show("impossible de prendre 1 rubis en plus");
                    }
                    else
                    {

                    }
                    if (threeCoins == 3)
                    {
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                        MessageBox.Show("On peut retirer seulement 3 pierres différentes");
                    }
                    else
                    {
                        cmdValidateChoice.Visible = true;
                        cmdResetChoice.Visible = true;
                        lblChoiceSaphir.Visible = true;
                        if (aviableSaphir == 2)
                        {
                            MessageBox.Show("Le stock ne peut pas descendre en dessous de 2");
                        }
                        else
                        {
                            if (nbSaphir == 2)
                            {
                                MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                            }
                            else
                            {
                                aviableSaphir--;
                                nbSaphir++;
                                lblSaphirCoin.Text = aviableSaphir.ToString();
                                lblChoiceSaphir.Text = nbSaphir + "\r\n";
                            }
                        }
                    }
                }
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
                if (nbRubis == 2 || nbSaphir == 2 || nbEmeraude == 2 || nbDiamand == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;
                    MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                }
                else
                {
                    // total de coins pris
                    threeCoins = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
                    if (threeCoins == 3)
                    {
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                        MessageBox.Show("On peut retirer seulement 3 pierres différentes");
                    }
                    else
                    {
                        cmdValidateChoice.Visible = true;
                        cmdResetChoice.Visible = true;
                        lblChoiceOnyx.Visible = true;
                        if (aviableOnyx == 2)
                        {
                            MessageBox.Show("Le stock ne peut pas descendre en dessous de 2");
                        }
                        else
                        {

                            if (nbOnyx == 2)
                            {
                                MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                            }
                            else
                            {
                                aviableOnyx--;
                                nbOnyx++;
                                lblOnyxCoin.Text = aviableOnyx.ToString();
                                lblChoiceOnyx.Text = nbOnyx + "\r\n";
                            }
                        }
                    }
                }
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
                if (nbRubis == 2 || nbSaphir == 2 || nbOnyx == 2 || nbDiamand == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;
                    MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                }
                else
                {
                    // total de coins pris
                    threeCoins = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
                    if (threeCoins == 3)
                    {
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                        MessageBox.Show("On peut retirer seulement 3 pierres différentes");
                    }
                    else
                    {
                        cmdValidateChoice.Visible = true;
                        cmdResetChoice.Visible = true;
                        lblChoiceEmeraude.Visible = true;
                        if (aviableEmeraude == 2)
                        {
                            MessageBox.Show("Le stock ne peut pas descendre en dessous de 2");
                        }
                        else
                        {
                            if (nbEmeraude == 2)
                            {
                                MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                            }
                            else
                            {
                                aviableEmeraude--;
                                nbEmeraude++;
                                lblEmeraudeCoin.Text = aviableEmeraude.ToString();
                                lblChoiceEmeraude.Text = nbEmeraude + "\r\n";
                            }
                        }
                    }
                }
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
                if (nbRubis == 2 || nbSaphir == 2 || nbOnyx == 2 || nbEmeraude == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;
                    MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                }
                else
                {
                    // total de coins pris 
                    threeCoins = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
                    if (threeCoins == 3)
                    {
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                        MessageBox.Show("On peut retirer seulement 3 pierres différentes");
                    }
                    else
                    {
                        cmdValidateChoice.Visible = true;
                        cmdResetChoice.Visible = true;
                        lblChoiceDiamand.Visible = true;
                        if (aviableDiamand == 2)
                        {
                            MessageBox.Show("Le stock ne peut pas descendre en dessous de 2");
                        }
                        else
                        {
                            if (nbDiamand == 2)
                            {
                                MessageBox.Show("Il est impossible de retirer plus de 2 pierres identiques");
                            }
                            else
                            {
                                aviableDiamand--;
                                nbDiamand++;
                                lblDiamandCoin.Text = aviableDiamand.ToString();
                                lblChoiceDiamand.Text = nbDiamand + "\r\n";
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
            //pour l'instant l'id du joeur est en dure ATTENTION : Il devra changer quand on peut entrez des joueurs -------------------------------
            int id = 0;

            //PlayerCoins var for Show and Back to BD
            int PlayerRubisCoin = 0;
            int PlayerSaphirCoin = 0;
            int PlayerOnyxCoin = 0;
            int PlayerEmeraudeCoin = 0;
            int PlayerDiamandCoin = 0;
            //int TotPlayerCoins = 0;

            //Instentiation Player
            Player player = new Player();

            player.Coins = conn.GetPlayerCoins(id); //take PlayerCoins to BD

            //total coins player
            PlayerRubisCoin = player.Coins[0] + nbRubis;
            PlayerSaphirCoin = player.Coins[3] + nbSaphir;
            PlayerOnyxCoin = player.Coins[2] + nbOnyx;
            PlayerEmeraudeCoin = player.Coins[1] + nbEmeraude;
            PlayerDiamandCoin = player.Coins[4] + nbDiamand;

            //Show PlayerCoins
            lblPlayerRubisCoin.Text = PlayerRubisCoin.ToString();
            lblPlayerSaphirCoin.Text = PlayerSaphirCoin.ToString();
            lblPlayerOnyxCoin.Text = PlayerOnyxCoin.ToString();
            lblPlayerEmeraudeCoin.Text = PlayerEmeraudeCoin.ToString();
            lblPlayerDiamandCoin.Text = PlayerDiamandCoin.ToString();

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

            //Back PlayerCoins to BD ------------------------------------------------------------------------------------------------
            conn.BackPlayerCoins(id);

            //Next Player
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
        /// click on the next player to tell him it is his turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button
        }

        private void lblChoiceRubis_Click(object sender, EventArgs e)
        {
            //Don't used
        }
        /// <summary>
        /// Reset all variables used for coins
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdResetChoice_Click(object sender, EventArgs e)
        {
            nbRubis = 0;
            nbOnyx = 0;
            nbEmeraude = 0;
            nbDiamand = 0;
            nbSaphir = 0;

            aviableRubis = 7;
            aviableSaphir = 7;
            aviableDiamand = 7;
            aviableOnyx = 7;
            aviableEmeraude = 7;

            //Reset label
            lblRubisCoin.Text = aviableRubis.ToString();
            lblSaphirCoin.Text = aviableSaphir.ToString();
            lblOnyxCoin.Text = aviableOnyx.ToString();
            lblEmeraudeCoin.Text = aviableEmeraude.ToString();
            lblDiamandCoin.Text = aviableDiamand.ToString();

            //Reset threeCoins
            threeCoins = 0;

            //label invisible
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceEmeraude.Visible = false;
            lblChoiceDiamand.Visible = false;

            //label enable
            lblRubisCoin.Enabled = true;
            lblSaphirCoin.Enabled = true;
            lblOnyxCoin.Enabled = true;
            lblEmeraudeCoin.Enabled = true;
            lblDiamandCoin.Enabled = true;
        }
    }
}
