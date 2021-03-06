<script>
import { HTTP } from "../api/http-common";
import { ref } from "vue";

export default {
  data() {
    return {
      id: 0,
      name: null,
      gender: null,
      birthDate: "0001-01-01T00:00:00",
      address: null,
      textInputOptions: "",
      popupCreateItem: 0,
    };
  },
  props: ["data", "popup"],
  emits: ["modalClose", "patientsRefresh"],
  methods: {
    async save() {
      if (!this.id) {
        let response = await HTTP.post(`/api/patients`, {
          name: this.name,
          gender: this.gender,
          birthDate: this.birthDate,
          address: this.address,
        });

        if (response.data) {
          this.$toast.success(`Created.`);
          this.popupCreateItem = 0;
          setTimeout(this.$toast.clear, 100);
        }
      } else {
        let response = await HTTP.put(`/api/patients/${this.id}`, {
          Id: this.id,
          name: this.name,
          gender: this.gender,
          birthDate: this.birthDate,
          address: this.address,
        });

        //if (response.data) {
        let instance = this.$toast.success(`Updated.`);
        //setTimeout(instance.dismiss(), 100);
        this.$toast.clear;
        //}
      }
      this.refreshClose();
    },
    refreshClose() {
      this.$emit("patientsRefresh");
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
    this.name = this.data.name ?? "";
    this.gender = this.data.gender ?? "";
    this.birthDate = new Date(this.data.birthDate);
    this.address = this.data.address ?? "";
  },
  setup() {},
};
</script>

<template>
  <div class="mt-10 sm:mt-0 py-2">
    <span
      class="text-lg float-right cursor-pointer ml-5"
      v-if="this.popup"
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
              :href="`/patients/${this.id}`"
              class="text-blue-500 hover:text-blue-900"
              >Patient #{{ this.id }}</a
            >
          </h3>
          <h3 class="text-lg font-medium leading-6 text-gray-900" v-else>
            <span>Create New Patient</span>
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
                    for="name"
                    class="block text-sm font-medium text-gray-700"
                    >Patient Name</label
                  >
                  <input
                    type="text"
                    name="name"
                    id="name"
                    v-model="this.name"
                    :class="
                      this.isSingleItem || this.popup
                        ? 'mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                        : 'pointer-events-none outline-none mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                    "
                  />
                </div>
                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="gender"
                    class="block text-sm font-medium text-gray-700"
                    >Gender</label
                  >
                  <select
                    id="gender"
                    name="gender"
                    v-model="this.gender"
                    :class="
                      this.isSingleItem || this.popup
                        ? 'mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                        : 'pointer-events-none outline-none mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                    "
                  >
                    <option>Male</option>
                    <option>Female</option>
                  </select>
                </div>

                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="country"
                    class="block text-sm font-medium text-gray-700"
                    >Date</label
                  >
                  <Datepicker
                    :class="
                      this.isSingleItem || this.popup
                        ? ''
                        : 'pointer-events-none outline-none'
                    "
                    v-model="this.birthDate"
                    :textInputOptions="textInputOptions"
                  />
                </div>

                <div
                  class="col-span-6 sm:col-span-3"
                  v-if="this.isSingleItem || this.popup"
                >
                  <label
                    for="address"
                    class="block text-sm font-medium text-gray-700"
                    >Patient Address</label
                  >
                  <input
                    type="text"
                    v-model="this.address"
                    name="address"
                    id="address"
                    :class="
                      this.isSingleItem || this.popup
                        ? 'mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                        : 'pointer-events-none outline-none mt-1 block w-full py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
                    "
                  />
                </div>
              </div>
            </div>
            <div
              class="px-4 py-3 bg-gray-50 text-right sm:px-6"
              v-if="this.isSingleItem || this.popup"
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
