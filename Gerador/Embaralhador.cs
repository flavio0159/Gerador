using System;
using System.Collections.Generic;

namespace Gerador
{
    public static class Embaralhador
    {
        /// <summary> 
        /// Embaralha uma Lista. 
        /// </summary> 
        /// <typeparam name="T">Tipo da Lista.</typeparam> 
        /// <param name="list">Lista que será embaralhada.</param> 
        /// <returns>A Lista embaralhada.</returns> 
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            // -> Gerador de numeros ramdomicos. 
            var r = new Random();

            // -> Percorre a Lista para embaralhar. 
            for (int i = 0; i < list.Count; i++)
            {
                // -> Adiquire um indice ramdomico 
                //    Observe que é passado o contador de indices da lista, 
                //    como indice máximo e Exclusivo! 
                //    (ou seja, o máximo não é incluso nos numeros randomicos). 
                var j = r.Next(list.Count);
                // -> Pega o item da posicao temporaria. 
                T obj = list[j];

                // -> Coloca o item da posição "i" na posição aleatória "j". 
                list[j] = list[i];

                // -> coloca o item aleatorio na posição "i". 
                list[i] = obj;
            }

            // -> Retorna a lista embaralhada. 
            return list;
        }
    }
}