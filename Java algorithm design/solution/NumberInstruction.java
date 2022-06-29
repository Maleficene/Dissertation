package uk.ac.aber.cs21120.calc.solution;

import uk.ac.aber.cs21120.calc.interfaces.IInstruction;

import java.util.Stack;

public class NumberInstruction implements IInstruction {
    private double value;

    public NumberInstruction(double value) {
        this.value = value;
    }

    @Override
    public void execute(Stack<Double> stack) { // pushes double precision floating point value onto the stack
        stack.push(value);
    }

    @Override
    public String toString() {
        return "NUMBER-" + Double.toString(value); // + string representation of the number
    }

    @Override
    public int getPrecedence() {
        return 0;
    }
}
