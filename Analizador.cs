using AngouriMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AngouriMath.Entity;
using static AngouriMath.MathS;

namespace EntregaFinalCalculoInf
{
    public class Analizador
    {
        Entity funcion;

        bool hayError = false;
        bool hayFuncion = false;
        string mensajeError = "";

        int cantidadfactores = 0;
        Entity[] factores;
        int[] prioridades;

        string tipo_u;
        Entity u;
        Entity du;

        string tipo_dv;
        Entity v;
        Entity dv;

        Entity uv;
        Entity vdu;

        Entity segunda;
        Entity final;
        Entity simplificada;

        public Analizador(string funcion)
        {
            // Verifica que el campo de texto no esté vacío
            if (string.IsNullOrEmpty(funcion))
            {
                this.mensajeError = "ERROR: La expresión no es una funcion valida";
                this.hayError = true;

                return;
            }

            this.funcion = funcion;

            this.hayFuncion = true;

            if (!this.funcion.Vars.Contains("x"))
            {
                this.mensajeError = "ERROR: La función debe contener la variable 'x'";
                this.hayError = true;
            }
            else if (!(this.funcion is Entity.Mulf))
            {
                this.mensajeError = "ERROR: La expresión debe ser una multiplicación";
                this.hayError = true;
            }
            else
            {
                SeleccionarPartesPorILATE();
                this.du = this.u.Differentiate("x").Simplify();
                this.v = this.dv.Integrate("x");
                this.uv = (this.u * this.v);
                this.vdu = (this.v * this.du);
                this.segunda = this.vdu.Simplify().Integrate("x");
                this.final = (this.uv - this.segunda);
                this.simplificada = this.final.Simplify();
            }
        }

        public List<string> getPasos()
        {
            // Creamos una lista para almacenar cada paso como un string
            var pasos = new List<string>();

            // Agregamos los pasos

            // Mostrar la integral original
            if (hayFuncion)
            {
                if (this.hayError)
                {
                    pasos.Add("Expresión original: " + this.funcion.ToString());
                }
                else
                {
                    pasos.Add("Integral original: ∫ " + this.funcion.ToString() + " dx");
                }
            }
            
            // Si hay errrores, los muestra
            if (this.hayError)
            {
                pasos.Add(this.mensajeError);
                return pasos;
            }

            // Lista de factores y su clasificación ILATE
            string p = "La integral consta de " + this.cantidadfactores + " factores";
            for (int f = 0; f < this.cantidadfactores; f++)
            {
                p += "\n- El factor " + this.factores[f].ToString() + " es una expresión " + this.ObtenerNombreTipo(this.prioridades[f]) + " (Prioridad ILATE " + this.prioridades[f] + ")";
            }
            pasos.Add(p);

            // Mostrar eleccion de u
            pasos.Add("Eleccion de u (factor de mayor prioridad): " + this.u.ToString());

            // Mostrar derivada de u
            pasos.Add("calculo de du: " + this.du.ToString());

            // Mostrar eleccion de dv
            pasos.Add("Eleccion de dv: " + this.dv.ToString());

            // Mostrar integral de dv
            pasos.Add("calculo de v: " + this.v.ToString());

            // Mostrar aplicacion de la formula por partes
            pasos.Add("Aplicamos la formula: " + this.uv.ToString() + " - ∫ " + this.vdu.ToString() + " dx");

            // Resolvemos la segunda integral
            pasos.Add("Resolucion de la segunda integral: " + this.segunda.ToString());

            // Resultado final antes de simplificar
            pasos.Add("Resultado final: " + this.final.ToString() + " + C");

            // Resultado final simplificado
            pasos.Add("Resultado final simplificado: " + this.simplificada.ToString() + " + C");

            // Mensaje de final
            pasos.Add("*** Fin ***");

            // Devolvemos la lista completa de pasos generados
            return pasos; 
        }

        // Clasificar segun ILATE 
        int ClasificarILATE(Entity expr)
        {
            // Funciones inversas trigonométricas
            if (expr is Entity.Arcsinf || expr is Entity.Arccosf || expr is Entity.Arctanf)
            {
                return 1;
            }

            // Funciones logarítmicas
            if (expr is Entity.Logf)
            {
                return 2;
            }

            // Funciones trigonométricas
            if (expr is Entity.Sinf || expr is Entity.Cosf || expr is Entity.Tanf)
            {
                return 4;
            }

            // Funciones exponenciales (e^x)
            if (expr is Entity.Powf)
            {
                Entity.Powf potencia = (Entity.Powf)expr;
                string baseStr = potencia.Base.ToString();
                if (baseStr == "e" || baseStr == "2.71828182845905")
                {
                    return 5;
                }
            }

            // Todo lo demás es algebraico (x^n, variables, polinomios, raíces)
            // No necesitamos identificar explícitamente porque es el caso por defecto
            return 3;
        }

        string ObtenerNombreTipo(int prioridad)
        {
            if (prioridad == 1) return "Inversa";
            if (prioridad == 2) return "Logarítmica";
            if (prioridad == 3) return "Algebraica";
            if (prioridad == 4) return "Trigonométrica";
            if (prioridad == 5) return "Exponencial";
            return "Desconocida";
        }

        // Agrupar factores
        Entity AgruparFactores(Entity[] factores, int numFactores, int indiceSaltear)
        {
            Entity resultado = null;

            for (int i = 0; i < numFactores; i++)
            {
                if (i == indiceSaltear)
                {
                    continue;
                }

                if (resultado == null)
                {
                    resultado = factores[i];
                }
                else
                {
                    resultado = resultado * factores[i];
                }
            }

            return resultado;
        }

        // Seleccion u y dv
        void SeleccionarPartesPorILATE()
        {
            this.u = null;
            this.dv = null;

            Entity.Mulf mult = (Entity.Mulf) this.funcion;

            // Contar factores
            this.cantidadfactores = 0;
            foreach (var factor in mult.DirectChildren)
            {
                this.cantidadfactores++;
            }

            // Crear arrays para factores y prioridades
            this.factores = new Entity[this.cantidadfactores];
            this.prioridades = new int[this.cantidadfactores];

            // Llenar los arrays
            int index = 0;
            foreach (var factor in mult.DirectChildren)
            {
                this.factores[index] = factor;
                this.prioridades[index] = ClasificarILATE(factor);

                string nombreTipo = ObtenerNombreTipo(prioridades[index]);

                index++;
            }

            // Encontrar el factor con mayor prioridad (menor número)
            int indiceMasPrioritario = 0;
            int menorPrioridad = prioridades[0];

            for (int i = 1; i < this.cantidadfactores; i++)
            {
                if (this.prioridades[i] < menorPrioridad)
                {
                    menorPrioridad = this.prioridades[i];
                    indiceMasPrioritario = i;
                }
            }

            // Asignar u (el más prioritario)
            u = factores[indiceMasPrioritario];

            // Asignar dv
            if (this.cantidadfactores == 2)
            {
                // Caso simple: solo 2 factores
                dv = this.factores[1 - indiceMasPrioritario];
            }
            else if (this.cantidadfactores > 2)
            {
                dv = AgruparFactores(this.factores, this.cantidadfactores, indiceMasPrioritario);
            }
            else
            {
                return;
            }
        }
    }
}
