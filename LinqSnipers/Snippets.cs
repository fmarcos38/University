using LinqSnipers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LinqSnippets
{
    public class Snippets
    {
        static public void StringLinq()
        {
            //creo lista/arreglo --> de string
            string[] cars =
            {
                "VW Gol",
                "Ford Escort",
                "Ford Ka",
                "Audi A3",
                "Peugeot 207"
            };

            //1. selecciono todos los cars --> en sql SELECT * 
            //creo una variable y mediante cod sql le asigno los cars
            var carList = from car in cars select car;

            //la recorro con cod c#
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }


            //2. SELECT WHERE car is Ausi-> filtro solo Ford
            var fordList = from car in cars where car.Contains("Ford") select car; //Contains("Ford") estoy manejando cadena de string

            foreach (var car in carList)
            {
                Console.WriteLine(fordList);
            }

        }

        //lista con numros
        static public void NumbersLinq()
        {
            List<int> numbers = new List<int> { 1,2,3,4,5,6,7,8,9};

            //mult todos x3 menos el 9 y los ordeno
            var listX3 =
                numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);//ascendente

            foreach(var num in listX3)
            {
                Console.WriteLine(num);
            }
        }

        //
        static public void EjDeBusquedas()
        {
            List<string> textList= new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "cj",
                "f",
                "c"
            };

            //encontrar el 1ro de los elemntos
            var searchFirst = textList.First();

            //busco el 1er elemnt q comienza con "c"
            var firtsC = textList.First(text => text.Equals("c"));
            //busco todos los q comienzan con c
            var allC = textList.All(text => text.Equals("c"));
            //1er elemnt q contenga la j
            var contJ = textList.First(text => text.Contains("j"));
            //1er elemnt q cont z o sino return vacio
            var firtsZ = textList.FirstOrDefault(text => text.Contains("z"));
            //ultimo q cont z o vacio
            var lastZ = textList.LastOrDefault(text => text.Contains("z"));
            //omite repetidos
            var unicos = textList.Single();

            //creo 2 listas de num 
            int[] numPares = { 0, 2, 4, 6, 8 };
            int[] otros = { 0, 2, 6 };

            //muestro los q no se repiten
            var filtrorepetidos = numPares.Except(otros);
        }

        
        //funciones para multiples selecciones
        static public void SeleccMultiples()
        {
            string[] miOpiniones =
            {
                "Titulo, texto",
            };

            //voy a separar Por la coma
            var separo = miOpiniones.SelectMany(texto => texto.Split(","));


            //ejemplos con objetos
            //creo array de objetos --> Empresa y c/empresa -> array de Employee
            var enterprises = new[]
            {
                new Enterprice()//instancio new objet
                {
                    Id = 1,
                    Name = "Enterp 1",
                    Employess = new [] //array de objts
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Pepe",
                            Email = "pepe@hot.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Jose",
                            Email = "pepe@hot.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Pepe",
                            Email = "pepe@hot.com",
                            Salary = 2000
                        },
                    }
                },
                new Enterprice()
                {
                    Id = 2,
                    Name = "Enterp 2",
                    Employess = new [] //array de objts
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "pepe@hot.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Maria",
                            Email = "pepe@hot.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Sol",
                            Email = "pepe@hot.com",
                            Salary = 2000
                        }
                    }
                }
            };

            //obt todos los empleados
            var allEmp = enterprises.SelectMany(e => e.Employess);

            //compruebo q tenga empresas
            bool hayEmpresas = enterprises.Any();

            //compruebo q tenga empleados
            bool hayEmpleados = enterprises.Any(e => e.Employess.Any());

            //busco empleados con salario mas de 1000
            bool salari1000 =
                enterprises.Any(emp =>
                emp.Employess.Any(empleado => empleado.Salary < 2000));
        }
         

        //colecciones
        static public void linqCollections()
        {
            //lista1
            var firstList = new List<string> { "a", "b", "c"};
            //lista2
            var secondList = new List<string> { "a", "d", "c" };

            //inner join SOLO los comunes
            var comunes = from element in firstList
                          join elemnts2 in secondList
                          on element equals elemnts2
                          select new { element, elemnts2};


            //outer join - left --> esto se entiende mediante conjuntos
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElemnt in temporalList.DefaultIfEmpty()
                                where element != temporalElemnt
                                select new { Element = element };

            //outer join - left --> esto se entiende mediante conjuntos
            //opcion menos codigo
            var left2 = from element in firstList
                        from secondElement in secondList.Where(sE => sE == element).DefaultIfEmpty()
                        select new {Element = element, SecondElement = secondElement };

            //outer join - right --> esto se entiende mediante conjuntos
            var rigthOuterJoin = from secondElement in secondList
                                join element in firstList
                                on secondElement equals element
                                into temporalList
                                from temporalElemnt in temporalList.DefaultIfEmpty()
                                where secondElement != temporalElemnt
                                select new { Element = secondElement };


            //union --> osea todos los elemntos (menos la intersección) --> VER conjuntos
            var unionList = leftOuterJoin.Union(rigthOuterJoin); //uso lista de arriba
        }

        
        //saltear o descartar elementos
        static public void SaltarElemnts()
        {
            var lista = new[] { 1, 2, 3, 4, 5 };


            var saltaEl2 = lista.Skip(2);//1,3,4,5
            var saltaLosUltimos2 = lista.SkipLast(2);
            var saltaMayores4 = lista.SkipWhile(num => num < 4);// 5 6 7

            //tomar opuesto a lo anterior
            var tomoHastaEl2 = lista.Take(2); //{1,2}
            var tomoLos2Ultimos = lista.TakeLast(2); //{4,5}
            var tomoMenoresA4 = lista.TakeWhile(num => num < 4);

        }


        //TODO -> Hace (en ingles)

        //variables

        //funcion ZIP

        //Repeat

        //ALL

        //Aggregate

        //District

        //GroupBy

    }
}