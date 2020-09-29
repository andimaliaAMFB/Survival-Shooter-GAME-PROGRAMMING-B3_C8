using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPatter
{
    public class InputHandler : MonoBehaviour
    {
        //the box we control with key
        public Transform boxTrans;
        
        //public different keys we need
        private Command buttonW, buttonS, buttonA, buttonD, buttonB, buttonZ, buttonR;

        //store all comands for replay and undo
        public static List<Command> oldCommands = new List<Command>();

        //box start position to know where replay begins
        private Vector3 boxStartPos;

        //to reset the coroutine
        private Coroutine replayCoroutine;

        //if we should star the replay
        public static bool shouldStartReplay;

        //so we cant press keys while replaying
        private bool isReplaying;

        void Start()
        {
            //bind keys with commands
            buttonB = new DoNothing();
            buttonW = new MoveForward();
            buttonS = new MoveReverse();
            buttonA = new MoveLeft();
            buttonD = new MoveRight();
            buttonZ = new UndoCommand();
            buttonR = new ReplayCommand();

            boxStartPos = boxTrans.position;
        }

        void Update()
        {
            if (!isReplaying)
                HandleInput();

            StartReplay();
        }

        //check if we press a key, if so do what the key is binded to
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
                buttonA.Execute(boxTrans, buttonA);
            else if (Input.GetKeyDown(KeyCode.B))
                buttonB.Execute(boxTrans, buttonB);
            else if (Input.GetKeyDown(KeyCode.D))
                buttonD.Execute(boxTrans, buttonD);
            else if (Input.GetKeyDown(KeyCode.R))
                buttonR.Execute(boxTrans, buttonR);
            else if (Input.GetKeyDown(KeyCode.S))
                buttonS.Execute(boxTrans, buttonS);
            else if (Input.GetKeyDown(KeyCode.W))
                buttonW.Execute(boxTrans, buttonW);
            else if (Input.GetKeyDown(KeyCode.Z))
                buttonZ.Execute(boxTrans, buttonZ);
        }

        //checks if we hould start the replay
        void StartReplay()
        {
            if (shouldStartReplay && oldCommands.Count > 0)
            {
                shouldStartReplay = false;

                //stop the coroutine so it strars from the beginning
                if (replayCoroutine != null)
                    StopCoroutine(replayCoroutine);

                //start the replay
                replayCoroutine = StartCoroutine(ReplayCommands(boxTrans));
            }
        }

        //the replay coroutine
        IEnumerator ReplayCommands(Transform boxTrans)
        {
            //so we cant move the box with keys while replaying
            isReplaying = true;

            //move the box to the start position
            boxTrans.position = boxStartPos;

            for (int i = 0; i < oldCommands.Count; i++)
            {
                //move the box with the current command
                oldCommands[i].Move(boxTrans);

                yield return new WaitForSeconds(0.3f);
            }

            //we can move box again
            isReplaying = false;
        }
    }
}
