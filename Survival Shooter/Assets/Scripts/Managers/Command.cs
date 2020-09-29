using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute();
    public abstract void UnExecute();

    public class MoveCommand : Command
    {
        PlayerMovement playerMovement;
        float h, v;

        public MoveCommand(PlayerMovement _playerMovement, float _h, float _v)
        {
            this.playerMovement = _playerMovement;
            this.h = _h;
            this.v = _v;
        }

        //tigger perintah movement
        public override void Execute()
        {
            playerMovement.Move(h, v);

            //menganimasikan player
            playerMovement.Animating(h, v);
        }

        public override void UnExecute()
        {
            //invers arah dari movement plaer
            playerMovement.Move(-h, -v);

            //menganimasikan player
            playerMovement.Animating(h, v);
        }
    }

    public class ShootCommand : Command
    {
        PlayerShooting playerShooting;

        public ShootCommand(PlayerShooting _playerShooting)
        {
            playerShooting = _playerShooting;
        }

        public override void Execute()
        {
            //player menembak
            playerShooting.Shoot();
        }

        public override void UnExecute()
        {
            
        }
    }
}
