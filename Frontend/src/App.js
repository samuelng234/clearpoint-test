import './App.css'
import { Container, Row, Col } from 'react-bootstrap'
import React, { useState, useEffect } from 'react'
import AddTodoItem from './components/AddTodoItem'
import TodoItemsContent from './components/TodoItemsContent'
import TodoToast from './components/TodoToast'
import Header from './components/Header'

const App = () => {
  const [showToast, setShowToast] = useState(false);
  const [toastMessage, setToastMessage] = useState("");
  const [toastColour, setToastColour] = useState("");

  const showToastMessage = (message, colour) => {
    setShowToast(true);
    setToastMessage(message);
    setToastColour(colour);
  }

  const closeToast = () => {
    setShowToast(false);
  }
  
  return (
    <div className="App">
      <TodoToast text={toastMessage} show={showToast} onClose={closeToast} backgroundColour={toastColour}></TodoToast>
      <Container>
        <Header></Header>
        <Row>
          <Col><AddTodoItem showToast={showToastMessage}></AddTodoItem></Col>
        </Row>
        <br />
        <Row>
          <Col><TodoItemsContent showToast={showToastMessage}></TodoItemsContent></Col>
        </Row>
      </Container>
      <footer className="page-footer font-small teal pt-4">
        <div className="footer-copyright text-center py-3">
          © 2021 Copyright:
          <a href="https://clearpoint.digital" target="_blank" rel="noreferrer">
            clearpoint.digital
          </a>
        </div>
      </footer>
    </div>
  )
}

export default App
