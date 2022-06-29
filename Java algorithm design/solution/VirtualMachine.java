package uk.ac.aber.cs21120.calc.solution;

import uk.ac.aber.cs21120.calc.interfaces.IInstruction;
import uk.ac.aber.cs21120.calc.interfaces.IVirtualMachine;

import java.util.Queue;
import java.util.Stack;

public class VirtualMachine implements IVirtualMachine {
    private Stack<Double> numbers = new Stack<>();

    @Override
    public void execute(Queue<IInstruction>instructions) { // : this will execute the queue of instructions provided, using a stack which must be a private field of VirtualMachine. It must clear the stack first.
        numbers.clear();
        for (IInstruction instruction: instructions) {
            instruction.execute(numbers);
        }

    }

    @Override
    public double getResult() { //this must return the value on top of the stack without removing it
        return numbers.peek();
    }
}
