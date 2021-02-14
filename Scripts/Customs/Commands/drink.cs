/*
 * Created by SharpDevelop.
 * User: Steve
 * Date: 09.07.2004
 * Time: 19:36
 */

using System; 
using System.Collections; 
using Server; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Scripts.Commands 
{
public class DrinkHeal 
  { 
   public static void Initialize() 
   { 
     Server.Commands.Register( "DrinkHeal", AccessLevel.Player, new CommandEventHandler( DrinkHeal_OnCommand ) ); 
   } 

   [Usage( "DrinkHeal" )] 
   public static void DrinkHeal_OnCommand( CommandEventArgs e ) 
   { 
     GreaterHealPotion m_GreaterHealPotion = (GreaterHealPotion)e.Mobile.Backpack.FindItemByType( typeof( GreaterHealPotion ) ); 
     HealPotion m_HealPotion = (HealPotion)e.Mobile.Backpack.FindItemByType( typeof( HealPotion ) );
     LesserHealPotion m_LesserHealPotion = (LesserHealPotion)e.Mobile.Backpack.FindItemByType( typeof( LesserHealPotion ) ); 
     int m_Exists = e.Mobile.Backpack.GetAmount( typeof( GreaterHealPotion ) );
   	int m_Existss = e.Mobile.Backpack.GetAmount( typeof( HealPotion ) );
   	int m_Existsss = e.Mobile.Backpack.GetAmount( typeof( LesserHealPotion ) );
      
      if ( m_Exists != 0 )
      {
      	 e.Mobile.SendMessage( "Heal Potion found" ); 
        
        m_GreaterHealPotion.OnDoubleClick(e.Mobile);
      }
      else if ( m_Existss != 0 )
       {
      	 e.Mobile.SendMessage( "Heal Potion found" ); 
        
        m_HealPotion.OnDoubleClick(e.Mobile);
       }
        else if ( m_Existsss != 0 )
       {
      	 e.Mobile.SendMessage( "Heal Potion found" ); 
        
        m_LesserHealPotion.OnDoubleClick(e.Mobile);
       }
      else 
      { 
        e.Mobile.SendMessage( "Cannot find Heal Potion" ); 
      } 
      
    } 
  } 

public class DrinkCure 
  { 
   public static void Initialize() 
   { 
     Server.Commands.Register( "DrinkCure", AccessLevel.Player, new CommandEventHandler( DrinkCure_OnCommand ) ); 
   } 

   [Usage( "DrinkCure" )] 
   public static void DrinkCure_OnCommand( CommandEventArgs e ) 
   { 
     GreaterCurePotion m_GreaterCurePotion = (GreaterCurePotion)e.Mobile.Backpack.FindItemByType( typeof( GreaterCurePotion ) ); 
     CurePotion m_CurePotion = (CurePotion)e.Mobile.Backpack.FindItemByType( typeof( CurePotion ) );
     LesserCurePotion m_LesserCurePotion = (LesserCurePotion)e.Mobile.Backpack.FindItemByType( typeof( LesserCurePotion ) ); 
     int m_Exists = e.Mobile.Backpack.GetAmount( typeof( GreaterCurePotion ) );
   	int m_Existss = e.Mobile.Backpack.GetAmount( typeof( CurePotion ) );
   	int m_Existsss = e.Mobile.Backpack.GetAmount( typeof( LesserCurePotion ) );
      
      if ( m_Exists != 0 )
      {
      	 e.Mobile.SendMessage( "Cure Potion found" ); 
        
        m_GreaterCurePotion.OnDoubleClick(e.Mobile);
      }
      else if ( m_Existss != 0 )
       {
      	 e.Mobile.SendMessage( "Cure Potion found" ); 
        
        m_CurePotion.OnDoubleClick(e.Mobile);
       }
        else if ( m_Existsss != 0 )
       {
      	 e.Mobile.SendMessage( "Cure Potion found" ); 
        
        m_LesserCurePotion.OnDoubleClick(e.Mobile);
       }
      else 
      { 
        e.Mobile.SendMessage( "Cannot find Cure Potion" ); 
      } 
      
    } 
  } 

public class DrinkRefresh 
  { 
   public static void Initialize() 
   { 
     Server.Commands.Register( "DrinkRefresh", AccessLevel.Player, new CommandEventHandler( DrinkRefresh_OnCommand ) ); 
   } 

   [Usage( "DrinkRefresh" )] 
   public static void DrinkRefresh_OnCommand( CommandEventArgs e ) 
   { 
     TotalRefreshPotion m_TotalRefreshPotion = (TotalRefreshPotion)e.Mobile.Backpack.FindItemByType( typeof( TotalRefreshPotion ) ); 
     RefreshPotion m_RefreshPotion = (RefreshPotion)e.Mobile.Backpack.FindItemByType( typeof( RefreshPotion ) );
    
     int m_Exists = e.Mobile.Backpack.GetAmount( typeof( TotalRefreshPotion ) );
   	int m_Existss = e.Mobile.Backpack.GetAmount( typeof( RefreshPotion ) );
   	
      
      if ( m_Exists != 0 )
      {
      	 e.Mobile.SendMessage( "Refresh Potion found" ); 
        
        m_TotalRefreshPotion.OnDoubleClick(e.Mobile);
      }
      else if ( m_Existss != 0 )
       {
      	 e.Mobile.SendMessage( "Refresh Potion found" ); 
        
        m_RefreshPotion.OnDoubleClick(e.Mobile);
       }
        
      else 
      { 
        e.Mobile.SendMessage( "Cannot find Refresh Potion" ); 
      } 
      
    } 
  } 

}
