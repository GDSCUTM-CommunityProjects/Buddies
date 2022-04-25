﻿using Buddies.API.Entities;
using Microsoft.Extensions.ML;
using Buddies.API.DataModels;

namespace Buddies.API.Services
{
    public class KnnService
    {
        /// <summary>
        /// Calculate distance between vector and project vector
        /// </summary>
        public static (double, double) Distance(double[] vector)
        {
            
            double distance = 0;
            double sim = 0;
            foreach (double vectorItem in vector)
            {
                distance += Math.Pow((vectorItem - 1), 2);
                sim += vectorItem;
            }
            return (Math.Pow(distance, 0.5), (double)sim/vector.Count());
        }

        public static double[] GetVector(Project a, Profile b)
        {

            double[] vector = new double[a.Skills.Count];
       
            foreach (var skill in b.Skills)
            {
                var i = 0;
                foreach (var projectSkill in a.Skills)
                {
                    string output = "";
                    double subsequenceTolerated = FuzzySearchService.LongestCommonSubsequence(projectSkill.Name.ToLower(), skill.Name.ToLower(), out output) / (double)projectSkill.Name.Length;
                    if (subsequenceTolerated > vector[i])
                    {
                        vector[i] = subsequenceTolerated;
                    }
                    i++;
                }

            }

            return vector;
        }

        public static List<(Profile, double)> KNearestUsers(Project a, List<Profile> users, int k, PredictionEnginePool<BuddyRating, BuddyRatingPrediction> predictionEnginePool)
        {

            List<(double, (Profile, double))> recommendations = new List<(double, (Profile, double))>();
            foreach (var user in users)
            {
                var vector = GetVector(a, user);
                var (dist, sim) = Distance(vector);
                var input = new BuddyRating { RaterId = a.Owner.Id, BeingRatedId = user.UserId };
                BuddyRatingPrediction prediction = predictionEnginePool.Predict(modelName: "BuddyRecommenderModel", example: input);
                recommendations.Add((dist + 0.01 * prediction.Score, (user, sim)));
            }
            recommendations.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            var kNearest = new List<(Profile, double)>();
            for (var i = 0; i < Math.Min(k, recommendations.Count); i++)
            {
                kNearest.Add(recommendations[i].Item2);

               
            }
            return kNearest;
        }

    }
}