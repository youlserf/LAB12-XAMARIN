using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMCalculator.ViewModels
{
    public class ViewModelCalculator : ViewModelBase
    {
        #region Propiedades
        int currentState = 1;
        string mathOperator;

        double firstNumber;
        public double FirstNumber
        {
            get { return firstNumber; }
            set
            {
                if (firstNumber != value)
                {
                    firstNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        double secondNumber;
        public double SecondNumber
        {
            get { return secondNumber; }
            set
            {
                if (secondNumber != value)
                {
                    secondNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        string result;
		public string Result
		{
			get {
                
                return result; }
			set
			{
				if (result != value)
				{
					result = value;
					OnPropertyChanged();
				}
			}
		}

        string resultAdd;
        public string ResultAdd
        {
            get
            {

                return resultAdd;
            }
            set
            {
                if (resultAdd != value)
                {
                    resultAdd = value;
                    OnPropertyChanged();
                }
            }
        }

        string processOperacion;

        public string ProcessOperacion
        {
            get
            {
                p_numeros.GetEnumerator();
                return processOperacion;
            }
            set
            {
                if (processOperacion != value)
                {
                    processOperacion = value;
                    OnPropertyChanged();
                }
            }
        }

        string resultOperacion;

        public string ResultOperacion
        {
            get
            {

                return resultOperacion;
            }
            set
            {
                if (resultOperacion != value)
                {
                    resultOperacion = value;
                    OnPropertyChanged();
                }
            }
        }

  

        ArrayList signos = new ArrayList();
        ArrayList p_numeros = new ArrayList();
       

        #endregion
        public ICommand OnSelectNumber { protected set; get; }
        public ICommand OnClear { protected set; get; }

        public ICommand Sumar { protected set; get; }
        public ICommand Restar { protected set; get; }
        public ICommand Dividir { protected set; get; }
        public ICommand Multiplicar { protected set; get; }
        public ICommand IgualTotal { protected set; get; }
        



        public ViewModelCalculator()
        {
            OnClear = new Command(() =>
            {
                firstNumber = 0;
                secondNumber = 0;
                currentState = 1;
                this.Result = "0";
                this.ResultOperacion = "0";
                this.ResultAdd = "0";
                p_numeros = new ArrayList();
                signos = new ArrayList();
            });

            OnSelectNumber = new Command<string>(
               execute: (string parameter) =>
               {
				   
				   string pressed = parameter;

				   if (Result == "0" || currentState < 0)
				   {
					   Result = "";
					   if (currentState < 0)
						   currentState *= -1;
				   }
                   ResultAdd += pressed;
				   Result += pressed;

				   double number;

				   if (double.TryParse(Result, out number))
				   {

					   Result = number.ToString("N0");
					   if (currentState == 1)
					   {
						   firstNumber = number;
					   }
					   else
					   {
						   secondNumber = number;
					   }
				   }
			   });

            Sumar = new Command(() =>
            {
                p_numeros.Add(ResultAdd);
                resultAdd = "";
                signos.Add("+");
                Result = Result + " " + "+" + " ";

            });

            Restar = new Command(() =>
            {
                p_numeros.Add(ResultAdd);
                resultAdd = "";
                signos.Add("-");
                Result = Result + " " + "-" + " ";
            });

            Multiplicar = new Command(() =>
            {
                p_numeros.Add(ResultAdd);
                resultAdd = "";
                signos.Add("*");
                Result = Result + " " + "*" + " ";

            });

            Dividir = new Command(() =>
            {
                if (result == "0")
                {
                    ResultOperacion = "0";
                   
                    return;
                }
                else
                {
                    p_numeros.Add(ResultAdd);
                    resultAdd = "";
                    signos.Add("/");
                    Result = Result + " " + "/" + " ";
                }


            });

            IgualTotal = new Command( () =>
            {
                p_numeros.Add(ResultAdd);
                resultAdd = "";
                var resultadochange = 0;
                int i = 0;
                foreach (var item in signos) {
                    int n1 = Int32.Parse((string)p_numeros[i]);
                    int n2 = Int32.Parse((string)p_numeros[i+1]);
                    if (item == "+") {
                        resultadochange =  n1 + n2;
                    }
                    if (item == "-")
                    {
                        resultadochange = n1 - n2;
                    }
                    if (item == "*")
                    {
                        resultadochange = n1 * n2;
                    }
                    if (item == "/")
                    {
                        resultadochange = n1 / n2;
                    }
                }
                ResultOperacion = "" + resultadochange +"";
                resultadochange = 0;
            });

        }


    }
}
