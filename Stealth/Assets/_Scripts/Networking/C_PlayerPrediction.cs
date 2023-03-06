// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Riptide;

// public class C_PlayerPrediction : MonoBehaviour
// {
//      [SerializeField] private PlayerClient player;

//     [Header("Client Prediction:")]
//     /*Client prediction variables */
//     public float Distancetolerance = 0.02f; //The amount of distance in units that we will allow the client's prediction to drift from it's position on the server, before a correction is necessary. 
//     public float Snapdistance = 2f; //The amount of distance in units when we just snap to the server position.
//     public SimulationState serverSimulationState;   //Latest simulation state from the server
//     private static PlayerCMD DefaultInputState = new PlayerCMD();
//     [HideInInspector] public int CurrentSimulationTick = 0;
//     private const int STATE_CACHE_SIZE = 256;
//     public SimulationState[] simulationStateCache = new SimulationState[STATE_CACHE_SIZE];
//     public PlayerCMD[] inputStateCache = new PlayerCMD[STATE_CACHE_SIZE];
//     private int lastCorrectedTick;
//     public Vector3 MovementDirection;

   
    
//     //sending physics updates
//     private void FixedUpdate() {
//         if(player.IsLocal)
//         {
//             //CurrentSimulationTick = GameManager.Singleton.Tick;
//             player.clientinputs.tick = NetworkManagerClient.Singleton.clientPredictedTick;

//             PlayerCMD playerinputs;

    
//             playerinputs = player.clientinputs;
//             movementcontroller.Move(playerinputs);
//             playerweaponcontroller.DetermineWeaponState(playerinputs);
//             SendInput(playerinputs);

//             // Reconciliate if there's a message from the server.
//             if (serverSimulationState != null) Reconciliate();

//             // Get the current simulation state.
//             SimulationState simulationState = CurrentSimulationState(player.clientinputs);

//             clientSimulationState = simulationState;

//             // Determine the cache index based on on modulus operator.
//             int cacheIndex = NetworkManager.Singleton.clientPredictedTick % STATE_CACHE_SIZE;

//             // Store the SimulationState into the simulationStateCache 
//             simulationStateCache[cacheIndex] = simulationState;

//             // Store the ClientInputState into the inputStateCache
//             inputStateCache[cacheIndex] = player.clientinputs;
//             //CurrentSimulationTick++;
//         }
//     }

//     private void SendInput(PlayerCMD inputstate)
//     {
//         //send messages to the server, remember the order
//         Message message = Message.Create(MessageSendMode.Unreliable, (ushort)ClientToServerId.input);
//         message.Add(inputstate.forwardMove);
//         message.Add(inputstate.sideMove);
//         message.Add(inputstate.viewDirection);
//         message.Add(inputstate.jump);
//         message.Add(inputstate.sprint);
       
//         message.Add(inputstate.tick);
//         NetworkManagerClient.Singleton.Client.Send(message);
//     }
// }

// [System.Serializable]
// public class SimulationState
// {
//     public Vector3 position;
//     public Quaternion rotation;
//     public Vector3 velocity;
//     public PlayerState clientstate;
//     public int tick;
// }
