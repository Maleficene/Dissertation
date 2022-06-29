package uk.ac.aber.cs21120.calc.solution;

import uk.ac.aber.cs21120.calc.interfaces.IInstruction;

import java.util.Stack;

public class BracketInstruction implements IInstruction {
    @Override
    public void execute(Stack<Double> stack) {
        throw new RuntimeException();
    }

    @Override
    public int getPrecedence() {
        return 0; //lowest precedence
    }
}
