import { useEffect, useState } from "react";
import Button from "../../node_modules/react-bootstrap/esm/Button";
import Table from "../../node_modules/react-bootstrap/esm/Table";
import { getTodoItems, updateTodoItems } from "../apis/todoItemApi";

const TodoItemsContent = (props) => {
  const [items, setItems] = useState([]);

  useEffect(() => {
    getItems();
  }, [])

  async function getItems() {
    try {
      getTodoItems().then(data => {
        setItems(data?.data?.items);
      }).catch(e => {
        props.showToast(e.response.data.toString(), "danger");
      });
    } catch (error) {
      console.error(error)
    }
  }
  
  async function handleMarkAsComplete(item) {
    try {
      item.isCompleted = true;
      updateTodoItems(item).then(() => {
        props.showToast("The item was completed", "success");
        getItems();
      }).catch(e => {
        props.showToast(e.response.data.toString(), "danger");
      });
    } catch (error) {
      console.error(error)
    }
  }

  return (
    <>
      <h1>
        Showing {items.length} Item(s){' '}
        <Button variant="primary" className="pull-right" onClick={() => getItems()}>
          Refresh
        </Button>
      </h1>

      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>Description</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.description}</td>
              <td>
                <Button variant="warning" size="sm" onClick={() => handleMarkAsComplete(item)}>
                  Mark as completed
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  )
}

export default TodoItemsContent;