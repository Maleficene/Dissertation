package uk.ac.aber.cs21120.calc.solution;

import uk.ac.aber.cs21120.calc.interfaces.IInstruction;

import java.util.Stack;

public class MultiplyInstruction implements IInstruction {
    @Override
    public void execute(Stack<Double> stack) {
        Double val1 = stack.pop();
        Double val2 = stack.pop();
        Double result = val1 * val2;
        stack.push(result);
    }

    @Override
    public String toString() {
        return "MULTIPLY";
    }

    @Override
    public int getPrecedence() {
        return 20; //high precedence
    }
}
