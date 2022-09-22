import { useEffect, useState } from "react";
import Button from "../../node_modules/react-bootstrap/esm/Button";
import Col from "../../node_modules/react-bootstrap/esm/Col";
import Container from "../../node_modules/react-bootstrap/esm/Container";
import Form from "../../node_modules/react-bootstrap/esm/Form";
import Row from "../../node_modules/react-bootstrap/esm/Row";
import Stack from "../../node_modules/react-bootstrap/esm/Stack";
import { createTodoItem } from "../apis/todoItemApi";

const AddTodoItem = (props) => {
  const [description, setDescription] = useState('');

  const handleDescriptionChange = (event) => {
    setDescription(event.target.value);
  }

  async function handleAdd() {
    try {
      createTodoItem(description).then(() => {
        props.showToast("The item was added", "success");
      }).catch(e => {
        props.showToast(e.response.data.toString(), "danger");
      });
    } catch (error) {
      console.error(error)
    }
  }

  function handleClear() {
    setDescription('');
  }

  return (
    <Container>
      <h1>Add Item</h1>
      <Form.Group as={Row} className="mb-3" controlId="formAddTodoItem">
        <Form.Label column sm="2">
          Description
        </Form.Label>
        <Col md="9">
          <Form.Control
            type="text"
            placeholder="Enter description..."
            value={description}
            onChange={handleDescriptionChange}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3 offset-md-2" controlId="formAddTodoItem">
        <Stack direction="horizontal" gap={2}>
          <Button variant="primary" onClick={() => handleAdd()}>
            Add Item
          </Button>
          <Button variant="secondary" onClick={() => handleClear()}>
            Clear
          </Button>
        </Stack>
      </Form.Group>
    </Container>
  )
}

export default AddTodoItem;