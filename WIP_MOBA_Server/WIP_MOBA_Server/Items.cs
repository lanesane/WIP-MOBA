using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIP_MOBA_Server
{
    public enum Classification
    {
        Damage,
        Defense,
        Movement,
        Health,
        Energy
    };

    public struct Item
    {
        #region INFORMATION

        #region Declarations

        private String nameVal; // Item Name
        private String descVal; // Item Description
        private int costVal; // Item Cost
        private Classification classVal; // Item Class

        #endregion

        #region Initializers

        public String NAME
        {
            get
            {
                return nameVal;
            }
            set
            {
                if (value != null)
                    nameVal = value;
            }
        }

        public String DESCRIPTION
        {
            get
            {
                return descVal;
            }
            set
            {
                if (value != null)
                    descVal = value;
            }
        }

        public int COST
        {
            get
            {
                return costVal;
            }
            set
            {
                if (value > 0)
                    costVal = value;
            }
        }

        public Classification CLASS
        {
            get
            {
                return classVal;
            }
            set
            {
                if (Enum.IsDefined(typeof(Classification), value))
                    classVal = value;
            }
        }

        #endregion

        #endregion

        #region ATTRIBUTES

        #region Physical Attributes

        private float phy_DamVal; // Physical Damage
        private float phy_ArmorVal; // Physical Armor
        private float phy_dScaleVal; // Physical Damage Scaling

        #endregion

        #region Magic Attributes

        private float mg_DamVal; // Magic Damage
        private float mg_ArmorVal; // Magic Armor
        private float mg_dScaleVal; // Magic Damage Scaling

        #endregion

        #region Movement Attributes

        private float mv_baseSpeedVal; // Base Movement Speed
        private float mv_pSpeedVal; // Percent Speed
        private float mv_timeIncreaseVal; // Speed Increase for Allotted Time

        #endregion 

        #region Health Attributes

        private float hp_baseHealthVal; // Base Health
        private float hp_pHealthVal; // Percent Health
        private float hp_baseRegenVal; // Health Regen
        private float hp_maxRegenVal; // Max Health Regen

        #endregion

        #region Energy Attributes

        private float eg_baseEnergyVal; // Base Energy
        private float eg_instRegenVal; // Instant Energy Regen
        private float eg_timeRegenVal; // Energy Regen Over Time
        private float eg_frLevelVal; // Freeze Energy Level Over Time
        private float eg_maxRegenVal; // Max Energy Regen

        #endregion

        #region Physical Attributes Initializers

        public float phy_Damage
        {
            get
            {
                return phy_DamVal;
            }
            set
            {
                if (value > 0)
                    phy_DamVal = value;
            }
        }

        public float phy_Armor
        {
            get
            {
                return phy_ArmorVal;
            }
            set
            {
                if (value > 0)
                    phy_ArmorVal = value;
            }
        }

        public float phy_dScale
        {
            get
            {
                return phy_dScaleVal;
            }
            set
            {
                if (value > 0)
                    phy_dScaleVal = value;
            }
        }

        #endregion

        #region Magic Attributes Initializers

        public float mg_Damage
        {
            get
            {
                return mg_DamVal;
            }
            set
            {
                if (value > 0)
                    mg_DamVal = value;
            }
        }

        public float mg_Armor
        {
            get
            {
                return mg_ArmorVal;
            }
            set
            {
                if (value > 0)
                    mg_ArmorVal = value;
            }
        }

        public float mg_dScale
        {
            get
            {
                return mg_dScaleVal;
            }
            set
            {
                if (value > 0)
                    mg_dScaleVal = value;
            }
        }

        #endregion

        #region Movement Attributes Initializers

        public float mv_baseSpeed
        {
            get
            {
                return mv_baseSpeedVal;
            }
            set
            {
                if (value > 0)
                    mv_baseSpeedVal = value;
            }
        }

        public float mv_pSpeed
        {
            get
            {
                return mv_pSpeedVal;
            }
            set
            {
                if (value > 0)
                    mv_pSpeedVal = value;
            }
        }

        public float mv_timeIncrease
        {
            get
            {
                return mv_timeIncreaseVal;
            }
            set
            {
                if (value > 0)
                    mv_timeIncreaseVal = value;
            }
        }

        #endregion

        #region Health Attributes Initializers

        public float hp_baseHealth
        {
            get
            {
                return hp_baseHealthVal;
            }
            set
            {
                if (value > 0)
                    hp_baseHealthVal = value;
            }
        }

        public float hp_pHealth
        {
            get
            {
                return hp_pHealthVal;
            }
            set
            {
                if (value > 0)
                    hp_pHealthVal = value;
            }
        }

        public float hp_baseRegen
        {
            get
            {
                return hp_baseRegenVal;
            }
            set
            {
                if (value > 0)
                    hp_baseRegenVal = value;
            }
        }

        public float hp_maxRegen
        {
            get
            {
                return hp_maxRegenVal;
            }
            set
            {
                if (value > 0)
                    hp_maxRegenVal = value;
            }
        }

        #endregion

        #region Energy Attributes Initializers

        public float eg_baseEnergy
        {
            get
            {
                return eg_baseEnergyVal;
            }
            set
            {
                if (value > 0)
                    eg_baseEnergyVal = value;
            }
        }

        public float eg_instRegen
        {
            get
            {
                return eg_instRegenVal;
            }
            set
            {
                if (value > 0)
                    eg_instRegenVal = value;
            }
        }

        public float eg_timeRegen
        {
            get
            {
                return eg_timeRegen;
            }
            set
            {
                if (value > 0)
                    eg_timeRegenVal = value;
            }
        }

        public float eg_frLevel
        {
            get
            {
                return eg_frLevelVal;
            }
            set
            {
                if (value > 0)
                    eg_frLevelVal = value;
            }
        }

        public float eg_maxRegen
        {
            get
            {
                return eg_maxRegenVal;
            }
            set
            {
                if (value > 0)
                    eg_maxRegenVal = value;
            }
        }

        #endregion

        #endregion

        #region ABILITIES

        #endregion
    }

    public class Items
    {
        #region Item Declarations

        #region Damage Class

        public Item broadSword;
        public Item woodenSpear;

        #endregion

        #region Defense Class

        public Item roundShield;

        #endregion

        #region Movement Class

        public Item travellersBoots;

        #endregion

        #region Health Class

        public Item metalLocket;

        #endregion

        #region Energy Class

        public Item gore;

        #endregion

        #endregion

        public void LoadItems()
        {
            #region Damage Class

            #region -- BROADSWORD --

            broadSword = new Item();

            broadSword.NAME = "broadSword";
            broadSword.DESCRIPTION = "A basic item that deals base physical damage";
            broadSword.COST = 300;
            broadSword.CLASS = Classification.Damage;

            broadSword.phy_Damage = 4;

            #endregion

            #region -- WOODEN SPEAR --

            woodenSpear = new Item();

            woodenSpear.NAME = "Wooden Spear";
            woodenSpear.DESCRIPTION = "A basic item that deals base physical damage";
            woodenSpear.COST = 500;
            woodenSpear.CLASS = Classification.Damage;

            woodenSpear.phy_dScale = 0.05F; // Does 5% of other player's health in damage

            #endregion

            #endregion

            #region Defense Class

            #region -- ROUND SHIELD --

            roundShield = new Item();

            roundShield.NAME = "Round roundShield";
            roundShield.DESCRIPTION = "A basic item that guards against physical damage";
            roundShield.COST = 200;
            roundShield.CLASS = Classification.Defense;

            roundShield.phy_Armor = 1;

            #endregion

            #endregion

            #region Movement Class

            #region -- TRAVELLER'S BOOTS --

            travellersBoots = new Item();

            travellersBoots.NAME = "Traveller's travellersBoots";
            travellersBoots.DESCRIPTION = "A basic item that increases your movement speed value";
            travellersBoots.COST = 250;
            travellersBoots.CLASS = Classification.Movement;

            travellersBoots.mv_baseSpeed = 10;

            #endregion

            #endregion

            #region Health Class

            #region -- METAL LOCKET --

            metalLocket = new Item();
            
            metalLocket.NAME = "Metal Locket";
            metalLocket.DESCRIPTION = "This is a basic item that permanently increases the player's health.";
            metalLocket.COST = 200;
            metalLocket.CLASS = Classification.Health;

            metalLocket.hp_baseHealth = 200;

            #endregion

            #endregion

            #region Energy Class

            #region THE GORE

            gore = new Item();

            gore.NAME = "Gauntlet of Revitalizing Energy";
            gore.DESCRIPTION = "A basic item that instantly restores a base value of the player's total energy.";
            gore.COST = 250;
            gore.CLASS = Classification.Energy;

            gore.eg_instRegen = 50;

            #endregion

            #endregion
        }
    }
}
