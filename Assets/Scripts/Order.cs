public class Order {
    public string playerId;
    public int priority;
    public Action action;
    public int turn;

    public Order(string playerId, int priority, Action action, int turn) {
        this.playerId = playerId;
        this.priority = priority;
        this.action = action;
        this.turn = turn;
    }
}
