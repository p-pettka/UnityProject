using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BallInstruction
{
    Idle=0,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    RotateRight,
    RotateLeft,
    ScaleUp,
    ScaleDown
}

public class BallComponent : MonoBehaviour
{
    public float Speed = 1.0f;
    //public BallInstruction Instruction = BallInstruction.Idle;
    public List<BallInstruction> Instructions = new List<BallInstruction>();
    private int CurrentInstruction = 0;
    //private float TimeInInstruction = 0.0f;
    public float InstructionLength = 1.0f;
    private float CurrentLength = 0.0f;
    private Vector3 vecRotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*switch (Instruction)
        {
            case BallInstruction.MoveUp:
                transform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime;
                break;
            case BallInstruction.MoveDown:
                transform.position -= new Vector3(0, 1, 0) * Speed * Time.deltaTime;
                break;
            case BallInstruction.MoveLeft:
                transform.position -= new Vector3(1, 0, 0) * Speed * Time.deltaTime;
                break;
            case BallInstruction.MoveRight:
                transform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime;
                break;
            default:
                Debug.Log("Idle");
                break;
            case BallInstruction.RotateLeft:
                vecRotation += Vector3.forward * Speed;
                transform.rotation = Quaternion.Euler(vecRotation);
                break;
            case BallInstruction.RotateRight:
                vecRotation -= Vector3.forward * Speed;
                transform.rotation = Quaternion.Euler(vecRotation);
                break;
            case BallInstruction.ScaleUp:
                transform.localScale += Vector3.one * Speed * Time.deltaTime;
                break;
            case BallInstruction.ScaleDown:
                transform.localScale -= Vector3.one * Speed * Time.deltaTime;
                break;
        }*/
        if (CurrentInstruction < Instructions.Count)
        {
            //TimeInInstruction += Time.deltaTime;
            CurrentLength += Time.deltaTime;
            float RealSpeed = Speed * Time.deltaTime;

            switch (Instructions[CurrentInstruction])
            {
                case BallInstruction.MoveUp:
                    transform.position += Vector3.up * RealSpeed;
                    break;

                case BallInstruction.MoveDown:
                    transform.position += Vector3.down * RealSpeed;
                    break;

                case BallInstruction.MoveLeft:
                    transform.position += Vector3.left * RealSpeed;
                    break;

                case BallInstruction.MoveRight:
                    transform.position += Vector3.right * RealSpeed;
                    break;
                case BallInstruction.RotateRight:
                    vecRotation += Vector3.back * Speed;
                    transform.rotation = Quaternion.Euler(vecRotation);
                    break;
                case BallInstruction.RotateLeft:
                    vecRotation += Vector3.forward * Speed;
                    transform.rotation = Quaternion.Euler(vecRotation);
                    break;
                case BallInstruction.ScaleUp:
                    transform.localScale += Vector3.one * RealSpeed;
                    break;
                case BallInstruction.ScaleDown:
                    transform.localScale -= Vector3.one * RealSpeed;
                    break;
                default:
                    Debug.Log("Idle");
                    break;
            }

            if (CurrentLength > InstructionLength)
            {
                CurrentLength = 0.0f;
                //TimeInInstruction = 0.0f;
                ++CurrentInstruction;
            }
            
        }


    }
}
