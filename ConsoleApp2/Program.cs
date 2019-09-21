using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Algorithm challenge to find majority elements. Given a distance between stars and a galaxy
/// is all the stars within that distance, is there a galaxy that holds the majority of stars.
/// Uses a modified shortest path algorithm that recursively searches for a majority galaxy
/// </summary>
namespace ConsoleApp2
{
    class Star
    {
        long x;
        long y;
        Dictionary<Star, int> starCount = new Dictionary<Star, int>();
        int count;
        long distance;
        Star(long x, long y) {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Sets the distance between stars that constitue a galaxy
        /// </summary>
        /// <param name="distance"></param>
        void SetDistance(long distance) {
            this.distance = distance;
        }
        /// <summary>
        /// Finds if the star is part of a majorty galaxy given a star and an array of stars
        /// </summary>
        /// <param name="stars"></param>
        /// <param name="star"></param>
        /// <returns></returns>
       public static Star GetMajorityStar(Star[] stars, Star star) {
            long count = 0;
            foreach (Star currentStar in stars)
            {
                long starDistance = (long)(Math.Pow(currentStar.x - star.x, 2) + Math.Pow(currentStar.y - star.y, 2));

                if (starDistance < currentStar.distance) {
                    count++;
                }
            }
            if (stars.Length/2 < count)
            {
                return star;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the count of stars in a galaxy
        /// </summary>
        /// <param name="stars"></param>
        /// <param name="star"></param>
        /// <returns></returns>
        long GetStarCount(Star[] stars, Star star) {
            long count = 0;
            foreach (Star currentStar in stars)
            {
                long starDistance = (long)(Math.Pow(currentStar.x - star.x, 2) + Math.Pow(currentStar.y - star.y, 2));

                if (starDistance < currentStar.distance) {
                    count++;
                }
            }
            return count;
        }
        /// <summary>
        /// Finds if there is a galaxy that encompases a majority of stars in O(n) time
        /// </summary>
        /// <param name="stars"></param>
        /// <returns></returns>
        public static Star findMajorityGalaxy(Star[] stars) {
            
            Star left;
            Star right;
            Star[] leftStars;
            Star[] rightStars;
            if (stars == null || stars.Length == 0) return null;
            else if (stars.Length == 1)
            {
                return stars[0];
            }
            else {
                int mid = stars.Length / 2;
                leftStars = new Star[mid];
                for (int i = 0; i < mid; i++) {
                    leftStars[i] = stars[i];
                }
                rightStars = new Star[stars.Length - mid];
                int rightIndex = 0;
                for (int i = mid; i < stars.Length; i++)
                {
                    rightStars[rightIndex] = stars[i];
                    rightIndex++;
                }
                left = findMajorityGalaxy(leftStars);
                right = findMajorityGalaxy(rightStars);
            }
            if (left == null && right == null)
            {

                return null;
            }
            else if (left == null)
            {

                return GetMajorityStar(stars, right);

            }
            else if (right == null)
            {
                return GetMajorityStar(stars, left);

            }
            else {
                left = GetMajorityStar(stars, left);
                right = GetMajorityStar(stars, right);
                if (left == null && right == null)
                {
                    return null;
                }
                else if (left == null) {
                    return right;
                }
                else {
                    return left;
                }
            }
        }




        static void Main(string[] args)
        {
            String line = Console.ReadLine();
            String[] split = line.Split(' ');
            long distanceSquared = (long)Math.Pow(long.Parse(split[0]),2);
            long numStars = long.Parse(split[1]);
            Star[] stars = new Star[numStars];
            Dictionary<Star,long> counts = new Dictionary<Star,long>();
            Star star;



            for (int i = 0; i < numStars; i++) {
                line = Console.ReadLine();
                split = line.Split(' ');
                star = new Star(long.Parse(split[0]), long.Parse(split[1]));
                star.SetDistance(distanceSquared);
                stars[i] = star;
            }
            star = findMajorityGalaxy(stars);
            if (star == null)
            {
                Console.WriteLine("NO");
            }
            else {
               Console.WriteLine(star.GetStarCount(stars, star));
            }

            Console.ReadLine();

        }
    }
}
