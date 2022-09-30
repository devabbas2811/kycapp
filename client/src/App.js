import './App.css';

import { Form, Input, Button, Label, FormGroup, ListGroup, ListGroupItem, ListGroupItemHeading, ListGroupItemText } from 'reactstrap';
import { useState } from 'react';


function App() {
  const [isValidAaadhar, setValidAadhar] = useState();

  const [aadhar, setAadhar] = useState("");
  const [error, setError] = useState("");


  const handleSubmitForm = (e) => {

    e.preventDefault()

    const text = document.getElementById('aadhartext').value;
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ rawtext: text })
    };
    fetch('https://localhost:7032/api/Aadhar/findAadhar', requestOptions)
      .then(async response => {
        const isJson = response.headers.get('content-type')?.includes('application/json');
        const data = isJson && await response.json();

        // check for error response
        if (!response.ok) {
          // get error message from body or default to response status
          const error = (data[0] && data[0].message) || response.status;


          setError(error)

          setValidAadhar(false)
          return Promise.reject(error);
        } else {
          console.log(data)
          setAadhar()

          setValidAadhar(true)
        }


      })
      .catch(error => {
        console.log(error)
        error = `Error: ${error}`;
        console.error('There was an error!', error);
      });

  }

  return (
    <div className="App">
      <Form onSubmit={(e) => handleSubmitForm(e)}>
        <FormGroup>
          <Label for="aadhartext"> Place your aadhar here...</Label>
          <Input id="aadhartext" name="aadhartext" type="textarea" />
        </FormGroup>

        <Button>Submit</Button>
      </Form>
      {!isValidAaadhar &&
        <>
          <ListGroup>
            <ListGroupItem active>
              <ListGroupItemHeading>
                {error}
              </ListGroupItemHeading>
            </ListGroupItem>
          </ListGroup>
        </>

      }

      {isValidAaadhar &&
        <>
          <ListGroup>
            <ListGroupItem active>
              <ListGroupItemHeading>
                { }
              </ListGroupItemHeading>
            </ListGroupItem>
          </ListGroup>
        </>

      }


    </div>
  );
}

export default App;
