using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Enums
{
    public enum RefundTypes
    {
        DamagedStock,
        WrongStock,
        Regret,
        TypingError
    }
    public enum ActionTypes
    {
        Login,
        Logoff,
        LoginFailed,
        ForgotPaswword,
        Register,
        RegisterFailed,
        AddItem,
        AddItemFailed,
        RemoveItem,
        AddStock,
        RemoveStock,
        Sale,
        StartUp,
        Exit,
        Crush
    }
    public enum TxtBoxNames
    {
        TxtKeepLogged,
        TxtIForgot,
        TxtRegister
    }
    public enum UserTypes
    {
        Admin,
        Manager,
        SalesMan,
        No_User = 500
    }
    public enum ItemTypes
    {
        Clothe,
        Ball,
        Net,
        Bat
    }
    public enum BallTypes
    {
        Soccer,
        Tennis,
        FootBall,
        BasketBall,
        BaseBall,
        GolfBall,
        PingPongBall,
        VolleyBall
    }
    public enum NetTypes
    {
        PingPong,
        Tennis,
        VolleyBall
    }
    public enum BatTypes
    {
        BaseBall,
        PingPong,
        Tennis,
        Golf
    }
    public enum ClotheTypes
    {
        Shirt,
        Pants,
        Shorts,
        Shoes
    }
    public enum ColorTypes
    {
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        White
    }
    public enum ShirtSizeTypes
    {
        Extra_Small,
        Small,
        Medium,
        Large,
        Extra_Large
    }
}
