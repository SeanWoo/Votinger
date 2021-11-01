import { type } from 'os';
import React from 'react';

interface IUser {
    login: string;
    password: string;
    age?: number;
};

interface IBestUser extends IUser {
    asdad: number;
}

interface IPerson {
    name: string;
    age: number | undefined;
    getInfo() : string;
}

class Person implements IPerson {
    public name: string;
    public age: number | undefined;

    constructor(user: IUser){
        this.name = user.login;
        this.age = user.age;
    }

    getInfo() : string {
        return this.name + " : " + this.age;
    }
}

var user: IUser = {
    login: "asdasd",
    password: "1231",
}

var bestUser: IBestUser = {
    login: "vasya",
    password: "1231",
    age: 45,
    asdad: 7345
}

const Test1: React.FC = () => {

    var person = new Person(user); 
    var bestPerson = new Person(bestUser);

    return (
        <div>
            <h1>person</h1>
            <h4>name: {person.name}</h4>
            <h4>age: {person.age}</h4>
            <br></br>
            <h1>bestPerson</h1>
            <h4>name: {bestPerson.name}</h4>
            <h4>age: {bestPerson.age}</h4>
            <br></br>
            {[1,2,3,4,5].map((value, id) => (
                <div>
                    <h1>{value}</h1>
                    <h1>{value}</h1>
                    <h1>{value}</h1>
                    <br></br>
                </div>
            ))}
        </div>
        
    );
}

export default Test1;