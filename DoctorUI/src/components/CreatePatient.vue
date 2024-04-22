<script setup>
import {ref} from "vue";

const ssn = ref('');
const mail = ref('');
const name = ref('');

const createPatient = async () => {
  const patient = {
    Ssn: ssn.value,
    Mail: mail.value,
    Name: name.value,
    Measurement: null // Could be omitted from the PostPatientDtom, also Measurement[S]
  };

  const request = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(patient),
  };
  let requestUrl = 'http://localhost:8081/Patient';

  //Get the result from the server
  const response = await fetch(requestUrl, request);
  const result = await response.json();
  console.log(result);
}
</script>

<template>
  <section>
    <label for="ssn">SSN:</label>
    <input v-model="ssn" type="text" id="ssn" name="ssn" required>
    <label for="mail">Mail:</label>
    <input v-model="mail" type="email" id="mail" name="mail" required>
    <label for="name">Name:</label>
    <input v-model="name" type="text" id="name" name="name" required>
    <button @click="createPatient">Create</button>
  </section>
</template>

<style scoped>
section {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>
