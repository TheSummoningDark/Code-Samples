package runningthings;

import java.io.BufferedReader;

import java.io.IOException;
import java.io.InputStreamReader;

import java.util.*;

public class Dungeon {
    HashMap<Integer, ArrayList<Pair>> adjList;
    int lastVertex;

    public Dungeon() {
        adjList = new HashMap<>();
        lastVertex = 0;

    }

    public void addIntersection(int intersection) {

        if (!adjList.containsKey(intersection)) {
            adjList.put(intersection, new ArrayList<>());
        }
        if (intersection > lastVertex) {
            lastVertex = intersection;
        }

    }

    public void addDungeonCorridor(int firstVertex, int secondVertex, double weight) {

        Pair edge = new Pair(secondVertex, weight);

        adjList.get(firstVertex).add(edge);

        edge = new Pair(firstVertex, weight);
        adjList.get(secondVertex).add(edge);

    }

    public void journeyThroughDungeon() {
        PriorityQueue<NodePair> priorityQueue = new PriorityQueue<>(Comparator.reverseOrder());

        double[] rayGunMultiplier = new double[10000];
        ArrayList<Pair> edges;
        priorityQueue.add(new NodePair(1.0000, 0));
        rayGunMultiplier[0] = 1;
        while (priorityQueue.size() > 0) {
            int u = priorityQueue.poll().vertex;
            if (adjList.containsKey(u)) {
                edges = adjList.get(u);
                for (Pair edge : edges) {
                    if (rayGunMultiplier[edge.getKey()] < rayGunMultiplier[u] * edge.getValue()) {
                        rayGunMultiplier[edge.getKey()] =rayGunMultiplier[u] * edge.getValue();

                        priorityQueue.add(new NodePair(rayGunMultiplier[edge.getKey()], edge.getKey()));


                    }
                }
            }

        }
        String largestSize = String.format("%.4f", rayGunMultiplier[lastVertex]);
        System.out.println(largestSize);

    }

    public class Pair{

        Integer key;
        Double weight;

        public Pair(Integer key, Double weight){
            this.key = key;
            this.weight = weight;

        }
        public int getKey(){
            return this.key;
        }
        public Double getValue(){
            return this.weight;
        }

    }

    public class NodePair implements Comparable<NodePair> {

        Double weight;
        int vertex;

        public NodePair(Double weight, int vertex) {
            this.weight = weight;
            this.vertex = vertex;

        }

        @Override
        public int compareTo(NodePair o) {
            return this.weight.compareTo(o.weight);
        }
    }


    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        String[] tokens = reader.readLine().split(" ");


        Dungeon dungeon;

        while (Integer.parseInt(tokens[0]) > 0 && Integer.parseInt(tokens[1]) > 0) {
            int N = Integer.parseInt(tokens[1]);
            dungeon = new Dungeon();
            for (int i = 0; i < N; i++) {

                tokens = reader.readLine().split(" ");
                int firstIntersection = Integer.parseInt(tokens[0]);
                int secondIntersection = Integer.parseInt(tokens[1]);
                dungeon.addIntersection(firstIntersection);
                dungeon.addIntersection(secondIntersection);
                dungeon.addDungeonCorridor(firstIntersection, secondIntersection, Double.parseDouble(tokens[2]));
            }

            dungeon.journeyThroughDungeon();
            tokens = reader.readLine().split(" ");
        }

    }
}

