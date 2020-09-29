using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Command;

public class InputHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;

    //queue untuk menyimpan list command
    Queue<Command> commands = new Queue<Command>();

    void FixedUpdate()
    {
        //menghandle input movement
        Command moveCommand = InputMovementHandling();
        if (moveCommand != null)
        {
            commands.Enqueue(moveCommand);

            moveCommand.Execute();
        }
    }

    void Update()
    {
        //menghandle shoot
        Command shootCommand = InputShootHandling();
        if(shootCommand != null)
        {
            shootCommand.Execute();
        }
    }

    Command InputMovementHandling()
    {
        //check jika movement sesuai dengan keynya
        if(Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, -1, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(playerMovement, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(playerMovement, 0, -1);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            //undo movement
            return Undo();
        }
        else
        {
            return new MoveCommand(playerMovement, 0, 0);
        }
    }

    Command Undo()
    {
        //jika queue command tidak kosong, lakukan perintah undo
        if(commands.Count > 0)
        {
            Command undoCommand = commands.Dequeue();
            undoCommand.UnExecute();
        }
        return null;
    }

    Command InputShootHandling()
    {
        //jika menembak trigger shoot command
        if(Input.GetButtonDown("Fire1"))
        {
            return new ShootCommand(playerShooting);
        }
        else
        {
            return null;
        }
    }
}
