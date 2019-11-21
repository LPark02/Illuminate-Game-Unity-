using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Compilar_v3 : MonoBehaviour
{
    public Button botaoCompila;

    public GameObject errorMessage;
    public TextMeshProUGUI textBox;
    public TMP_InputField inputTMP;
    public string[] linhas;
    private string[] sequence_commands;
    static string keepLinhas;    

    public PlayerMov player_target;
    public GameObject playerObj;

    public float castingTime = 1.5f;

    public CollisionSensor objFrente;
    public CollisionSensor objAtras;
    public CollisionSensor objDentro;

    private bool contemFimSe = false;

    private void Awake()
    {
        inputTMP.text = keepLinhas;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("keeplin:"+ keepLinhas);
            inputTMP.text = keepLinhas;
        }
    }

    //Quando o botão Compilar for pressionado
    public void Compile()
    {
        errorMessage.SetActive(false);
        botaoCompila.interactable = false;
        keepLinhas = textBox.text;        
        linhas = textBox.text.Split('\n');
        int numero_linhas = linhas.Length;
        int i = 0;
        sequence_commands = new string[linhas.Length + 50];
        //sequence_commands = new string[linhas.Length];

        //Tratamento de letras maiusculas
        //Tratamento_Minusc();

        //verifica os comandos comput´´aveis
        Leitura_Comandos(i);
        if (errorMessage.activeSelf == false)
        {
            StartCoroutine(_execute());
        }
        else
        {
            botaoCompila.interactable = true;
        }
    }
    //realiza a execução dos comandos
    public IEnumerator _execute()
    {
        for(int numLinha = 0; numLinha < sequence_commands.Length - 1; numLinha++)
        {                        
            if (sequence_commands[numLinha] == null)
            {
                break;
            }            
            else if (sequence_commands[numLinha].StartsWith("se("))
            {
                string[] conds = new string[] { "", "" };
                GameObject objeto1;
                GameObject objeto2;

                int startIndexSe = numLinha;
                int endIndexSe = startIndexSe;
                int k = startIndexSe + 1;

                //define posicao do fimse
                for (int kk = startIndexSe; kk < sequence_commands.Length; kk++)
                {                    
                    if(sequence_commands[kk] == null)
                    {
                        //donothing
                        break;
                    }
                    else if (!sequence_commands[kk].StartsWith("fimse"))
                    {
                        //donothing
                        //break;
                    }
                    else
                    {
                        endIndexSe = kk;
                        contemFimSe = true;
                        break;
                    }
                }

                if (sequence_commands[numLinha].Contains("=="))
                {
                    Condicional(conds, "==", numLinha);
                    if (VerifSeExistem(conds))
                    {
                        objeto1 = GameObject.Find(conds[0]);
                        print("obj1: "+ objeto1);
                        objeto2 = GameObject.Find(conds[1]);
                        print("obj2: " + objeto2);
                        if (objeto1.name == "sensor_frente" || objeto1.name == "sensor_atras" || objeto1.name == "sensor_dentro" ||
                            objeto2.name == "sensor_frente" || objeto2.name == "sensor_atras" || objeto2.name == "sensor_dentro")
                        {                                                        
                            if (!contemFimSe)
                            {
                                print("Nao contem fimse");
                                Erro();
                            }
                            if (objeto1.GetComponent<CollisionSensor>().hasObjInside) //objeto1.GetComponent<CollisionSensor>().hasObjInside
                            {
                                print("objeto1:" +objeto1.GetComponent<CollisionSensor>().objectInside.name);
                                print("objeto2:" + objeto2.name);
                                if (objeto1.GetComponent<CollisionSensor>().objectInside.name != objeto2.name)
                                {
                                    numLinha = endIndexSe +1;
                                }
                            }
                            else
                            {                               
                                numLinha = endIndexSe +1;
                            }
                        }
                        else
                        {
                            numLinha = endIndexSe +1;
                        }
                    }
                    else
                    {
                        numLinha = endIndexSe;
                    }                  
                }
                else if (sequence_commands[numLinha].Contains("!="))
                {
                    Condicional(conds, "!=", numLinha);
                    if (VerifSeExistem(conds))
                    {
                        objeto1 = GameObject.Find(conds[0]);
                        print("obj1: " + objeto1);
                        objeto2 = GameObject.Find(conds[1]);
                        print("obj2: " + objeto2);
                        if (objeto1.name == "sensor_frente" || objeto1.name == "sensor_atras" || objeto1.name == "sensor_dentro")
                        {
                            if (!contemFimSe)
                            {
                                print("Nao contem fimse");
                                Erro();
                            }
                            if (objFrente.hasObjInside) //objeto1.GetComponent<CollisionSensor>().hasObjInside
                            {
                                print("objeto1:" + objeto1.GetComponent<CollisionSensor>().objectInside.name);
                                print("objeto2:" + objeto2.name);
                                if (objeto1.GetComponent<CollisionSensor>().objectInside.name == objeto2.name)
                                {                                    
                                    numLinha = endIndexSe;
                                }                                                                
                            }
                            else
                            {
                                numLinha = endIndexSe;
                            }
                        }
                        else
                        {
                            numLinha = endIndexSe;
                        }
                    }
                    else
                    {
                        numLinha = endIndexSe;
                    }
                }
                else
                {
                    Debug.Log("ERRO");
                    Erro();
                    break;
                }
            }
            else if (sequence_commands[numLinha].StartsWith("andar"))
            {
                player_target.andar();
                playerObj.GetComponent<Animator>().Play("Walk");
            }
            else if (sequence_commands[numLinha].StartsWith("voltar"))
            {
                player_target.voltar();
                playerObj.GetComponent<Animator>().Play("Walk");
            }
            else if (sequence_commands[numLinha].StartsWith("direita"))
            {
                player_target.viraDireita();
            }
            else if (sequence_commands[numLinha].StartsWith("esquerda"))
            {
                player_target.viraEsquerda();
            }
            else if (sequence_commands[numLinha].StartsWith("fimse"))
            {
                //do nothing
            }
            yield return new WaitForSeconds(castingTime);
        }
        //botaoCompila.interactable = true;
        yield return null;
    }

    //verifica os comandos comput´´aveis
    public void Leitura_Comandos(int ii)
    {
        Tratamento_Minusc();
        for (int j = 0; j < linhas.Length; j++)
        {
            if (linhas[j].StartsWith("andar"))
            {
                //player_target.andar_frente();                
                sequence_commands[ii] = linhas[j];
                ii++;
            }
            else if (linhas[j].StartsWith("voltar"))
            {
                //player_target.andar_frente();                
                sequence_commands[ii] = linhas[j];
                ii++;
            }
            else if (linhas[j].StartsWith("direita"))
            {
                //player_target.andar_frente();                
                sequence_commands[ii] = linhas[j];
                ii++;
            }
            else if (linhas[j].StartsWith("esquerda"))
            {
                //player_target.andar_frente();                
                sequence_commands[ii] = linhas[j];
                ii++;
            }
            else if (linhas[j].StartsWith("repita("))
            {
                //essa estrutura envia
                int startIndex = linhas[j].IndexOf("(");
                int endIndex = linhas[j].IndexOf(")");
                string paramNumLoops = linhas[j].Substring(startIndex + 1, endIndex - startIndex - 1);
                int numLoops = int.Parse(paramNumLoops);
                int startIndexRepita = j;

                for (int z = 0; z < numLoops; z++)
                {
                    int k = startIndexRepita + 1;                    
                    while (!linhas[k].Contains("fimrepita"))
                    {          
                        //caso nao tenha fimrepita
                        if(k >= linhas.Length - 1)
                        {
                            Debug.Log("ERRO");
                            Erro();
                            break;
                        }
                        if (linhas[k] == null)
                        {
                            break;
                        }
                        if (linhas[k].StartsWith("andar"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[ii] = linhas[k];
                            ii++;
                        }
                        else if (linhas[k].StartsWith("voltar"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[ii] = linhas[k];
                            ii++;
                        }
                        else if (linhas[k].StartsWith("direita"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[ii] = linhas[k];
                            ii++;
                        }
                        else if (linhas[k].StartsWith("esquerda"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[ii] = linhas[k];
                            ii++;
                        }

                        else if (linhas[j] == "\n")
                        {

                        }
                        else if (linhas[j] == "")
                        {

                        }
                        else
                        {
                            Debug.Log("ERRO");
                            Erro();
                            break;
                        }                        
                        k++;
                    }
                    j = k; //endIndexRepita

                }
            }
            else if (linhas[j].StartsWith("se("))
            {
                string[] conds = new string[] { "", "" };
                
                if (linhas[j].Contains("==") || linhas[j].Contains("!="))
                {
                    sequence_commands[ii] = linhas[j];
                    ii++;
                }
                //else if (linhas[j].Contains("diferente"))
                //{
                //    Condicional(conds, "diferente", j);
                //    if (VerifSeExistem(conds))
                //    {
                //        objeto1 = GameObject.Find(conds[0]);
                //        objeto2 = GameObject.Find(conds[1]);
                //    }
                //    else
                //    {
                //        Erro();
                //        break;
                //    }
                //}            
                else
                {
                    Debug.Log("ERRO");
                    Erro();
                    break;
                }
            }
            else if (linhas[j].StartsWith("fimse"))
            {
                sequence_commands[ii] = linhas[j];
                ii++;
            }
            else if (linhas[j] == "\n")
            {

            }
            else if (linhas[j] == "")
            {

            }
            else if(linhas[j] == null)
            {

            }
            else
            {
                print("lin: " + linhas[j]);
                Debug.Log("ERRO");
                errorMessage.SetActive(true);
                break;
            }
        }
    }

    public void Tratamento_Minusc()
    {
        for (int j = 0; j < linhas.Length; j++)
        {
            linhas[j] = linhas[j].ToLower();
        }
    }

    public string[] Condicional(string[] condicionais, string etcetc, int j)
    {
        int startIndex = linhas[j].IndexOf("(");
        int endIndex = linhas[j].IndexOf(")");

        string cond1;
        int cond1StartIndex = 0;
        int cond1EndIndex;
        cond1StartIndex = startIndex + 1;
        cond1EndIndex = linhas[j].IndexOf(etcetc) - 1;
        cond1 = linhas[j].Substring(cond1StartIndex, cond1EndIndex - cond1StartIndex);
        cond1.Trim();
        condicionais[0] = cond1;        

        string cond2;
        int cond2StartIndex;
        int cond2EndIndex;
        cond2StartIndex = linhas[j].IndexOf(etcetc) + etcetc.Length + 1;
        cond2EndIndex = endIndex;
        cond2 = linhas[j].Substring(cond2StartIndex, cond2EndIndex - cond2StartIndex);
        cond2.Trim();
        condicionais[1] = cond2;

        return condicionais;
    }

    public bool VerifSeExistem(string[] listaObjs)
    {
        print(listaObjs[0]);
        print(listaObjs[1]);
        if (GameObject.Find(listaObjs[0]) && GameObject.Find(listaObjs[1]))
        {
            return true;
        }
        else
        {
            print("Não existem");
            Erro();
            return false;
        }
    }

    public void Erro()
    {        
        errorMessage.SetActive(true);
    }

    public void LeituraDosComandos(int jj)
    {
        Tratamento_Minusc();
        for (int j = 0; j < linhas.Length; j++)
        {
            if (linhas[j].StartsWith("andar"))
            {
                //player_target.andar_frente();                
                sequence_commands[jj] = linhas[j];
                jj++;
            }
            else if (linhas[j].StartsWith("voltar"))
            {
                //player_target.andar_frente();                
                sequence_commands[jj] = linhas[j];
                jj++;
            }
            else if (linhas[j].StartsWith("direita"))
            {
                //player_target.andar_frente();                
                sequence_commands[jj] = linhas[j];
                jj++;
            }
            else if (linhas[j].StartsWith("esquerda"))
            {
                //player_target.andar_frente();                
                sequence_commands[jj] = linhas[j];
                jj++;
            }
            else if (linhas[j].StartsWith("repita("))
            {
                //essa estrutura envia
                int startIndex = linhas[j].IndexOf("(");
                int endIndex = linhas[j].IndexOf(")");
                string paramNumLoops = linhas[j].Substring(startIndex + 1, endIndex - startIndex - 1);
                int numLoops = int.Parse(paramNumLoops);
                int startIndexRepita = j;

                for (int z = 0; z < numLoops; z++)
                {
                    int k = startIndexRepita + 1;
                    while (!linhas[k].Contains("fimrepita"))
                    {
                        if (linhas[k] == null)
                        {
                            break;
                        }
                        if (linhas[k].StartsWith("andar"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[jj] = linhas[k];
                            jj++;
                        }
                        else if (linhas[k].StartsWith("voltar"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[jj] = linhas[k];
                            jj++;
                        }
                        else if (linhas[k].StartsWith("direita"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[jj] = linhas[k];
                            jj++;
                        }
                        else if (linhas[k].StartsWith("esquerda"))
                        {
                            //player_target.andar_frente();                
                            sequence_commands[jj] = linhas[k];
                            jj++;
                        }

                        else if (linhas[j] == "\n")
                        {

                        }
                        else if (linhas[j] == "")
                        {

                        }
                        else
                        {
                            Erro();
                            break;
                        }
                        k++;
                    }
                    j = k; //endIndexRepita

                }
            }            
            else if (linhas[j] == "\n")
            {

            }
            else if (linhas[j] == "")
            {

            }
            else if (linhas[j] == null)
            {

            }
            else
            {
                print("lin: " + linhas[j]);
                Debug.Log("ERRO");
                errorMessage.SetActive(true);
                break;
            }
        }
    }
}
