import axios from "../../node_modules/axios/index";

const client = axios.create({
  baseURL: process.env.REACT_APP_SERVER_BASEURL
});

export const createTodoItem = (description) => {
    return client.post("", 
        { 
            description: description, 
            isCompleted: false 
        });
}

export const getTodoItems = () => {
    return client.get("");
}

export const updateTodoItems = (todoItem) => {
    return client.put(todoItem.id, 
        { 
            description: todoItem.description, 
            isCompleted: todoItem.isCompleted 
        });
}