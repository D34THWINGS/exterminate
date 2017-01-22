public class Order {
    public string playerId;
    public int priority;
    public Action action;

    public Order(string playerId, int priority, Action action) {
        this.playerId = playerId;
        this.priority = priority;
        this.action = action;
    }
}
