package uk.ac.aber.cs21120.calc.solution;

import uk.ac.aber.cs21120.calc.interfaces.IInstruction;

import java.util.Stack;

public class SubtractInstruction implements IInstruction {
    @Override
    public void execute(Stack<Double> stack) {
        Double B = stack.pop(); //Pop a value from the stack, store in variable B
        Double A = stack.pop(); //Pop a value from the stack, store in variable A
        Double result = A - B;  //Push A subtract B
        stack.push(result);




    }

    @Override
    public String toString() {
        return "SUBTRACT";
    }

    @Override
    public int getPrecedence() {
        return 10; // low precedence same as AddInstruction
    }
}
