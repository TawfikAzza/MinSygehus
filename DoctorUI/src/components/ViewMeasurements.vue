<script setup>
import {ref} from "vue";

const ssn = ref('');
const measurements = ref([]);

const getMeasurements = async () => {
  console.log("Getting measurements");
  console.log(ssn.value);

  const request = {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  };
  let requestUrl = 'http://localhost:8081/Patient/?ssn=' + ssn.value + '&measurement=true';

  //Get the result from the server
  const response = await fetch(requestUrl, request);
  const result = await response.json();
  console.log(result);
  measurements.value = result.measurements;
}

const markAsSeen = async (id) => {
  console.log("Marking as seen");
  const measurement = measurements.value.find(m => m.id === id);
  measurement.seen = true;

  const request = {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(measurement),
  };
  let requestUrl = 'http://localhost:9091/Measurement';

  //Get the result from the server
  const response = await fetch(requestUrl, request);
  const result = await response.json();
  console.log(result);
  getMeasurements();
}

</script>

<template>
  <section>
    <label for="ssn">SSN:</label>
    <input v-model="ssn" type="text" id="ssn" name="ssn" required>
    <button @click="getMeasurements">Get Measurements</button>
    <div v-for="measurement in measurements" :key="measurement.id">
      <p>{{measurement.date}}</p>
      <p>{{measurement.systolic}}</p>
      <p>{{measurement.diastolic}}</p>
      <p v-if="measurement.seen">Seen</p>
      <button v-else @click="markAsSeen(measurement.id)">Mark as seen</button>
      _______________________
    </div>
  </section>
</template>

<style scoped>
section {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>
