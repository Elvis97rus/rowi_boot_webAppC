<script>
import { HTTP } from "../api/http-common";
import { ref } from "vue";

export default {
  data() {
    return {
      id: 0,
      visitDate: "",
      visitPlace: "",
      patient: "",
      doctor: "",
      medicine: "",
      medicines: "",
      diagnosis: "",
      recommendation: "",
      textInputOptions: "",
      isSingleItem: "",
      layout: "",
      popupCreateItem: 0,
    };
  },
  props: ["data", "popup", "doctors", "medicines", "patients"],
  emits: ["modalClose", "visitsRefresh"],
  methods: {
    async save() {
      if (!this.id) {
        console.log("post");
        let response = await HTTP.post(`/api/visits`, {
          name: this.name,
          gender: this.gender,
          birthDate: this.birthDate,
          address: this.address,
        });

        if (response.data) {
          this.$toast.success(`Created.`);
          this.popupCreateItem = 0;
        }
      } else {
        let response = await HTTP.put(`/api/visits/${this.id}`, {
          Id: this.id,
          visitDate: this.visitDate,
          visitPlace: this.visitPlace,
          patient: this.patient,
          doctor: this.doctor,
          medicine: this.medicine,
          diagnosis: this.diagnosis,
          recommendation: this.recommendation,
        });
        //if (response.data) {
        this.$toast.success(`Updated.`);
        //}
      }
      this.refreshClose();
    },
    refreshClose() {
      this.$emit("visitsRefresh");
      this.$emit("modalClose");
    },
  },
  mounted() {
    this.popupCreateItem = this.popup ?? 0;
    this.isSingleItem = this.$route.params.id ?? false;
    const textInputOptions = ref({
      format: "MM/dd/yyyy",
    });
    this.id = this.data.id ?? 0;
    this.visitDate = new Date(this.data.visitDate);
    this.visitPlace = this.data.visitPlace ?? "";
    this.patient = this.data.patient ?? 0;
    this.doctor = this.data.doctor ?? 0;
    this.medicine = this.data.medicine ?? 0;
    this.diagnosis = this.data.diagnosis ?? "";
    this.recommendation = this.data.recommendation ?? "";
  },
  setup() {},
};
</script>

<template>
  <div class="mt-10 sm:mt-0 py-2">
    <span
      class="text-lg float-right cursor-pointer ml-5"
      v-if="this.popupCreateItem"
      @click="this.refreshClose()"
      >&times;</span
    >
    <div class="md:grid md:grid-cols-3 md:gap-6">
      <div class="md:col-span-1 m-auto">
        <div class="px-4 sm:px-0">
          <h3
            class="text-lg font-medium leading-6 text-gray-900"
            v-if="this.id"
          >
            <a
              :href="`/visits/${this.id}`"
              class="text-blue-500 hover:text-blue-900"
              >Visit #{{ this.id }}</a
            >
          </h3>
          <h3 class="text-lg font-medium leading-6 text-gray-900" v-else>
            <span>Create New Visit</span>
          </h3>
          <p class="mt-1 text-sm text-gray-600">Fill the Information</p>
        </div>
      </div>
      <div class="mt-5 md:mt-0 md:col-span-2">
        <form method="POST">
          <div class="shadow overflow-hidden sm:rounded-md">
            <div class="px-4 py-5 bg-white sm:p-6">
              <div class="grid grid-cols-6 gap-6">
                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="patient"
                    class="block text-sm font-medium text-gray-700"
                    >Patient</label
                  >
                  <select
                    id="patient"
                    name="patient"
                    v-model="this.patient"
                    :class="
                      this.isSingleItem
                        ? 'mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                        : 'pointer-events-none outline-none mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                    "
                  >
                    <option
                      v-for="patient of this.patients"
                      :key="patient.id"
                      :value="patient.id"
                      :selected="patient.id == this.patient"
                    >
                      {{ patient.name }}
                    </option>
                  </select>
                </div>

                <div
                  class="col-span-6 sm:col-span-3"
                  v-if="this.isSingleItem || this.popupCreateItem"
                >
                  <label
                    for="doctor"
                    class="block text-sm font-medium text-gray-700"
                    >Doctor</label
                  >
                  <select
                    id="doctor"
                    name="doctor"
                    v-model="this.doctor"
                    :class="
                      this.isSingleItem
                        ? 'mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                        : 'pointer-events-none outline-none mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                    "
                  >
                    <option
                      v-for="doctor of this.doctors"
                      :key="doctor.id"
                      :value="doctor.id"
                      :selected="doctor.id == this.doctor"
                    >
                      {{ doctor.name }}
                    </option>
                  </select>
                </div>

                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="visitDate"
                    class="block text-sm font-medium text-gray-700"
                    >Visit Date</label
                  >
                  <Datepicker
                    id="visitDate"
                    v-model="this.visitDate"
                    :class="
                      this.isSingleItem
                        ? ''
                        : 'pointer-events-none outline-none'
                    "
                    :textInputOptions="textInputOptions"
                  />
                </div>

                <div
                  class="col-span-6 sm:col-span-3"
                  v-if="this.isSingleItem || this.popupCreateItem"
                >
                  <label
                    for="visit_place"
                    class="block text-sm font-medium text-gray-700"
                    >Visit Place</label
                  >
                  <input
                    type="text"
                    v-model="this.visitPlace"
                    name="visit_place"
                    id="visit_place"
                    class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md"
                  />
                </div>

                <div
                  class="col-span-6 sm:col-span-6 lg:col-span-2"
                  v-if="this.isSingleItem || this.popupCreateItem"
                >
                  <label
                    for="diagnosis"
                    class="block text-sm font-medium text-gray-700"
                    >Diagnosis</label
                  >
                  <input
                    type="text"
                    v-model="this.diagnosis"
                    name="diagnosis"
                    id="diagnosis"
                    class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md"
                  />
                </div>

                <div
                  class="col-span-6 sm:col-span-3"
                  v-if="this.isSingleItem || this.popupCreateItem"
                >
                  <label
                    for="medicine"
                    class="block text-sm font-medium text-gray-700"
                    >Medicine</label
                  >
                  <select
                    id="medicine"
                    name="medicine"
                    v-model="this.medicine"
                    class="mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                  >
                    <option
                      v-for="medicine of this.medicines"
                      :key="medicine.id"
                      :value="medicine.id"
                      :selected="medicine.id == this.medicine"
                    >
                      {{ medicine.name }}
                    </option>
                  </select>
                </div>
              </div>
              <div v-if="this.isSingleItem">
                <label
                  for="recommendation"
                  class="block text-sm font-medium text-gray-700"
                >
                  Recommendation
                </label>
                <div class="mt-1">
                  <textarea
                    v-model="this.recommendation"
                    id="recommendation"
                    name="recommendation"
                    rows="3"
                    class="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 mt-1 block w-full sm:text-sm border border-gray-300 rounded-md"
                  ></textarea>
                </div>
                <p class="mt-2 text-sm text-gray-500">
                  Brief description for your profile. URLs are hyperlinked.
                </p>
              </div>
            </div>
            <div
              class="px-4 py-3 bg-gray-50 text-right sm:px-6"
              v-if="this.isSingleItem || this.popupCreateItem"
            >
              <button
                type="submit"
                @click.prevent="save"
                class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                Save
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
