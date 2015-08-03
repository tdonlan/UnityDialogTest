﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



    public enum TreeType
    {
        World=0,
        Zone=1,
        Dialog=2,
        Quest=3,
        Battle=4,
        Info=5

    }

    public enum ZoneNodeType
    {
        Battle,
        Dialog,
        Store,
        Item,
        Info,
        Puzzle,
        Link,

    }

    public enum BattleNodeType
    {
        Info,
        Enemy,
        Loot,
        Win
    }

    public enum InfoNodeType
    {
        Info,
        Loot,
        End
    }

