using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;

    float timer;
    //Queue untuk menyimpan list command
    Queue<Command> commands = new Queue<Command>();

    void FixedUpdate()
    {
        //Menghandle input movement
        Command moveCommand = InputMovementHandling();
        if (moveCommand != null)
        {
            commands.Enqueue(moveCommand);

            moveCommand.Execute();
        }
    }

    void Update()
    {
        //Mengahndle shoot
        timer += Time.deltaTime;
        Command shootCommand = InputShootHandling();
        if (shootCommand != null && timer >= playerShooting.timeBetweenBullets)
        {
            timer = 0;
            shootCommand.Execute();
        }
        if (timer >= playerShooting.timeBetweenBullets * playerShooting.effectsDisplayTime)
        {
            playerShooting.DisableEffects();
        }
    }

    Command InputMovementHandling()
    {
        //Check jika movement sesuai dengan key nya
        //saya ubah agar bisa bergerak secara diagonal
        int x, y;
        x = 0;
        y = 0;

        //input arah gerakan menggunakan variabel x dan y , dimana y adalah gerakan vertikal , dan x adalah horizontal
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }

        if(x!=0 || y != 0)
        {
            return new MoveCommand(playerMovement, x, y);
        }

        else if (Input.GetKey(KeyCode.Z))
        {
            //Undo movement
            return Undo();
        }
        else
        {
            return new MoveCommand(playerMovement, 0, 0); ;
        }
    }

    Command Undo()
    {
        //Jika Queue command tidak kosong, lakukan perintah undo
        if (commands.Count > 0)
        {
            Command undoCommand = commands.Dequeue();
            undoCommand.UnExecute();
        }
        return null;
    }

    Command InputShootHandling()
    {
        //Jika menembak trigger shoot command
        if (Input.GetButton("Fire1"))
        {
            return new ShootCommand(playerShooting);
        }
        else
        {
            return null;
        }
    }
}
