package uk.ac.aber.cs21120.calc.solution;

import uk.ac.aber.cs21120.calc.interfaces.IInstruction;
import uk.ac.aber.cs21120.calc.interfaces.IParser;
import uk.ac.aber.cs21120.calc.interfaces.IVirtualMachine;
import uk.ac.aber.cs21120.calc.interfaces.SyntaxException;

import java.util.LinkedList;
import java.util.Queue;
import java.util.Stack;
import java.util.StringTokenizer;

public class Parser implements IParser {
    //Method to check if the token is a double
    public boolean isDouble( String input )
    {
        try
        {
            Double.parseDouble( input );
            return true;
        }
        catch( Exception e )
        {
            return false;
        }
    }

    @Override
    public Queue<IInstruction> parseExpression(String s) throws SyntaxException {
        //Construct a new tokenizer from the input string
        StringTokenizer tokenizer = new StringTokenizer(s, "()*/-+", true); // split strings into tokens
        //create an empty queue called output
        Queue<IInstruction> output = new LinkedList<>();
        //create an empty stack called operator
        Stack<IInstruction> operator = new Stack<>();
        //while there are tokens remaining
        while (tokenizer.hasMoreTokens()) {
            //read a token as a string
            String tok = tokenizer.nextToken();
            //if the token string represents a number
            if(isDouble(tok)){
                //convert the string to a double-precision floating point value
                double number = Double.parseDouble(tok);
                //create a NumberInstruction from that value and add it to the output queue
                output.add(new NumberInstruction(number));
                //else if the token string is a multiply, divide, add or subtract operator
            } else if ("*/-+".contains(tok)){
                //create a new instruction for that operator
                switch (tok) {
                    case "*":
                        while (!operator.empty() && operator.peek().getPrecedence() >= new MultiplyInstruction().getPrecedence()) {
                            //pop an item from the operator stack //add it to the output queue
                            output.add(operator.pop());
                        }
                        //push the new instruction onto the operator
                        operator.push(new MultiplyInstruction());
                        break;
                    case "+":
                        while (!operator.empty() && operator.peek().getPrecedence() >= new AddInstruction().getPrecedence()) {
                            //pop an item from the operator stack //add it to the output queue
                            output.add(operator.pop());
                        }
                        //push the new instruction onto the operator
                        operator.push(new AddInstruction());
                        break;
                    case "-":
                        while (!operator.empty() && operator.peek().getPrecedence() >= new SubtractInstruction().getPrecedence()) {
                            //pop an item from the operator stack //add it to the output queue
                            output.add(operator.pop());
                        }
                        //push the new instruction onto the operator
                        operator.push(new SubtractInstruction());
                        break;
                    default:
                        while (!operator.empty() && operator.peek().getPrecedence() >= new DivideInstruction().getPrecedence()) {
                            //pop an item from the operator stack //add it to the output queue
                            output.add(operator.pop());
                        }
                        //push the new instruction onto the operator
                        operator.push(new DivideInstruction());
                        break;
                }
            }else if("(".contains(tok)){
                operator.push(new BracketInstruction());
            }else if(")".contains(tok)){
                while (!operator.empty() && !(operator.peek() instanceof BracketInstruction)){
                    output.add(operator.pop());
                }
                if(operator.empty()){
                    throw new SyntaxException("Mismatched Bracket");
                }
                operator.pop();
                System.out.println("It's a bracket instruction");
            }else{//else
                throw new SyntaxException("Unrecognised Token");//throw a SyntaxException (unrecognised token)
            }
        }
        while (!operator.empty()){  //while the operator stack is not empty
            // pop an item from the operator stack // add it to the output queue
            IInstruction pop = operator.pop();
            if(pop.equals(new BracketInstruction())){
                throw new SyntaxException("Mismatched Brackets");
            }
            output.add(pop);
        }
        return output;
    }
}


