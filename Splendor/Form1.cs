/**
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
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        private int nbSaphir;

        private int aviableRubis = 7;
        private int aviableSaphir = 7;
        private int aviableDiamand = 7;
        private int aviableOnyx = 7;
        private int aviableEmeraude= 7;

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

            lblDiamandCoin.Text = aviableDiamand.ToString();
            lblEmeraudeCoin.Text = aviableEmeraude.ToString();
            lblOnyxCoin.Text = aviableOnyx.ToString();
            lblRubisCoin.Text = aviableRubis.ToString();
            lblSaphirCoin.Text = aviableSaphir.ToString();

            conn = new ConnectionDB();

            //load cards from the database
            //they are not hard coded any more
            //TO DO

       

            //load cards from the database
            Stack<Card> listCardOne;
            Stack<Card> listCardTwo;
            Stack<Card> listCardThree;

            //charge les cartes dans les cases
            listCardOne = conn.GetListCardAccordingToLevel(1);
            int nbDataInStack = listCardOne.Count;
            int i = 0;
            foreach (Control ctrl in flwCardLevel1.Controls)
            {
                if (i < nbDataInStack)
                {
                    ctrl.Text = listCardOne.Pop().ToString();
                    i++;
                }

            }

            listCardOne = conn.GetListCardAccordingToLevel(2);
            nbDataInStack = listCardOne.Count;
            i = 0;
            foreach (Control ctrl in flwCardLevel2.Controls)
            {
                if (i < nbDataInStack)
                {
                    ctrl.Text = listCardOne.Pop().ToString();
                    i++;
                }

            }

            listCardOne = conn.GetListCardAccordingToLevel(3);
            nbDataInStack = listCardOne.Count;
            i = 0;
            foreach (Control ctrl in flwCardLevel3.Controls)
            {
                if (i < nbDataInStack)
                {
                    ctrl.Text = listCardOne.Pop().ToString();
                    i++;
                }

            }


            //Go through the results
            //Don't forget to check when you are at the end of the stack

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

        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
            TextBox txtBox = sender as TextBox;

            MessageBox.Show(txtBox.Text);
           
           


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
            player.Coins = new int[] { 0, 1, 0, 1, 1 };

            lblPlayerDiamandCoin.Text = player.Coins[0].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[1].ToString();
            lblPlayerRubisCoin.Text = player.Coins[2].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[3].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[4].ToString();
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
                    threeCoins = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
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
            //TO DO Check if card or coins are selected, impossible to do both at the same time
            lblPlayerRubisCoin.Text = lblPlayerRubisCoin.Text + nbRubis.ToString();
            lblPlayerSaphirCoin.Text = lblPlayerSaphirCoin.Text + nbSaphir.ToString();
            lblPlayerOnyxCoin.Text = lblPlayerOnyxCoin.Text + nbOnyx.ToString();

            //10 coins max player

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
