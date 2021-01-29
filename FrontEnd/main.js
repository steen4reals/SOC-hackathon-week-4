const mainList = document.querySelector("#main-list");
const inputForm = document.querySelector("#input-form");

const BACKEND_URL = "http://localhost:5000";

async function loadInitialToDos() {
  const res = await fetch(`${BACKEND_URL}/items`);
  const data = await res.json();
  data.forEach(renderToDo);
}

function renderToDo(toDoItem) {
  const { id, title, priority, isComplete } = toDoItem;

  const span = document.createElement("span");
  span.innerText = `${title} - priority: ${priority}`;

  const li = document.createElement("li");
  li.id = `to-do-item-${id}`;
  li.appendChild(createCheckbox(toDoItem));
  li.appendChild(span);
  li.appendChild(createDeleteButton(toDoItem));
  if (isComplete) {
    li.classList.add("completed");
  }
  mainList.appendChild(li);
}

function createCheckbox(toDoItem) {
  const checkBox = document.createElement("input");
  checkBox.type = "checkbox";
  checkBox.addEventListener("change", () => {
    toggleToDoComplete(toDoItem);
  });
  return checkBox;
}

function createDeleteButton(toDoItem) {
  const button = document.createElement("button");
  button.innerText = "DELETE";
  button.addEventListener("click", () => {
    deleteToDo(toDoItem);
  });
  return button;
}

async function handleAddToDo(event) {
  // stops page reloading by default
  event.preventDefault();
  // get title and priority from form
  const partialTodo = {
    title: event.target[0].value,
    priority: event.target[1].value,
  };
  // sent to database and recieve full item including id
  const completeTodo = await sendToDo("/items", "POST", partialTodo);
  event.target.reset();
  renderToDo(completeTodo);
}

async function sendToDo(path, method, body = "") {
  const res = await fetch(`${BACKEND_URL}${path}`, {
    method,
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify(body),
  });
  const data = await res.json();
  return data;
}

async function toggleToDoComplete(toDoItem) {
  const li = document.querySelector(`#to-do-item-${toDoItem.id}`);
  await sendToDo(`/items/${toDoItem.id}`, "PUT", {
    ...toDoItem,
    isComplete: !li.classList.contains("completed"),
  });
  li.classList.toggle("completed");
}

async function deleteToDo(toDoItem) {
  const res = await fetch(`${BACKEND_URL}/items/${toDoItem.id}`, {
    method: "DELETE",
  });
  if (res.ok) {
    document.querySelector(`#to-do-item-${toDoItem.id}`).remove();
  }
}

inputForm.addEventListener("submit", handleAddToDo);

loadInitialToDos();


function compare(a, b){

  if (a.priority == "high" && b.priority == "medium" || a.priority == "high" && b.priority == "low" || a.priority == "medium" && b.priority == "low") {
    return -1;
  }
  
  if (a.priority == "medium" && b.priority == "high" || a.priority == "low" && b.priority == "high" ||a.priority == "low" && b.priority == "medium") {
    return 1;
  }
  
  return 0;
}

async function loadItems() {
  let lu_all = document.querySelectorAll("ul");
  lu_all.forEach((li)=>li.innerHTML="");

  const res = await fetch(`${BACKEND_URL}/items`);
  const data = await res.json();
  
  data.sort(compare);
  data.forEach(renderToDo);
}

let sortButton = document.querySelector("#sortButton");
//document.body.appendChild(sortButton);
sortButton.addEventListener("click", loadItems);

let deleteAllButton = document.querySelector("#deleteAllButton");
deleteAllButton.addEventListener("click", deleteAll);

//foreach through all li 
//if class = 'completed', then run delete to do