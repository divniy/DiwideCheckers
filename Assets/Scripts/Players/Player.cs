using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Diwide.Checkers
{
    public class Player
    {
        [Inject] private TilesRegistry _registry;
        public ColorType PawnsColor { get; }
        public List<PawnFacade> Pawns { get; private set; } = new();
        
        public Player(ColorType pawnsColor)
        {
            PawnsColor = pawnsColor;
        }

        public void StartTurn()
        {
            // SetPawns();
            foreach (var pawnFacade in Pawns)
            {
                pawnFacade.GenerateValidMoves();
            }
        }

        /*public void SetPawns()
        {
            Pawns.Clear();
            foreach (var tileFacade in _registry.TileFacades.Where(_=>_.PawnFacade != null && _.PawnFacade.Color == PawnsColor))
            {
                Pawns.Add(tileFacade.PawnFacade);
            }
        }*/
        public class Factory : PlaceholderFactory<ColorType, Player>
        {
            
        }
    }
}