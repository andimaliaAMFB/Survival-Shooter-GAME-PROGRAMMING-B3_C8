using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPatter
{
    //the parent class
    public abstract class Command
    {
        //how far should the box move when we press a button
        protected float moveDistance = 1f;

        //move and maybe save command
        public abstract void Execute(Transform boxTrans, Command command);

        //undo an old command
        public virtual void Undo(Transform boxTrans) { }

        //move the box
        public virtual void Move(Transform boxTrans) { }
    }

    //
    //child classes
    //

    public class MoveForward : Command
    {
        //called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //move the box
            Move(boxTrans);

            //save the command
            InputHandler.oldCommands.Add(command);
        }

        //undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.forward * moveDistance);
        }

        //move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.forward * moveDistance);
        }
    }

    public class MoveReverse : Command
    {
        //called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //move the box
            Move(boxTrans);

            //save the command
            InputHandler.oldCommands.Add(command);
        }

        //undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.forward * moveDistance);
        }

        //move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.forward * moveDistance);
        }
    }

    public class MoveLeft : Command
    {
        //called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //move the box
            Move(boxTrans);

            //save the command
            InputHandler.oldCommands.Add(command);
        }

        //undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.right * moveDistance);
        }

        //move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.right * moveDistance);
        }
    }

    public class MoveRight : Command
    {
        //called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //move the box
            Move(boxTrans);

            //save the command
            InputHandler.oldCommands.Add(command);
        }

        //undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.right * moveDistance);
        }

        //move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.right * moveDistance);
        }
    }

    //for keys with no binding
    public class DoNothing : Command
    {
        //called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //Nothing will happen if we press this key
        }
    }

    //undo one command
    public class UndoCommand : Command
    {
        public override void Execute(Transform boxTrans, Command command)
        {
            List<Command> oldCommands = InputHandler.oldCommands;

            if (oldCommands.Count > 0)
            {
                Command latestCommand = oldCommands[oldCommands.Count - 1];

                //move the box with this command
                latestCommand.Undo(boxTrans);

                //remove the command from the list
                oldCommands.RemoveAt(oldCommands.Count - 1);
            }
        }
    }

    //replay all commands
    public class ReplayCommand : Command
    {
        public override void Execute(Transform boxTrans, Command command)
        {
            InputHandler.shouldStartReplay = true;
        }
    }
}
